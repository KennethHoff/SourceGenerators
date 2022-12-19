﻿using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace LanguageFileParser.Generator;

// TODO: Convert to IIncrementalGenerator
// TODO: Add option to select output partial class name
[Generator]
public class LanguageFileTransformGenerator : ISourceGenerator
{
	private const string Authors = "Kenneth Hoff";
	private const string PackageName = "Oxx.Backend.Utils.Oxx.Backend.Generators.LanguageFileParser";

	private const string OutputPartialClassName = "Localizations";
	private const string FileExtension = ".xml";
	private const int TopLevelDepth = 0;

	private const string Delimiter = "/";


	private static readonly Regex LanguageFileRegex = new(@"^.*Resources(\\|/)LanguageFiles(\\|/).*.xml$");
	private static readonly Assembly ThisAssembly = typeof(LanguageFileTransformGenerator).Assembly;
	private static readonly Version AssemblyVersion = ThisAssembly.GetName().Version;

	private static readonly string HeaderTemplate =
		$"""
		/* 
		 * This file is auto-generated by { PackageName}
		 * Version: {AssemblyVersion}
		 * Authors: {Authors}
		 */
		""";

	#region Interface implementations

	public void Execute(GeneratorExecutionContext context)
	{
		context.AddSource("lol.g.cs", "public class Lol { }");
		// var languageFiles = context.AdditionalFiles
		// 	.Where(at => LanguageFileRegex.IsMatch(at.Path))
		// 	.ToImmutableList();
		//
		// languageFiles.ForEach(file =>
		// {
		// 	var content = file.GetText(context.CancellationToken);
		// 	if (content is null)
		// 	{
		// 		return;
		// 	}
		//
		// 	var elements = new List<ParsedElement>();
		//
		// 	var fileName = new Uri(file.Path).Segments.Last().Replace(FileExtension, string.Empty);
		// 	var parsed = ParseXmlContent(content, fileName, elements);
		// 	var sanitizedPath = SanitizePath(file.Path);
		// 	context.AddSource($"{sanitizedPath}.g.cs", parsed);
		// });
	}

	public void Initialize(GeneratorInitializationContext context)
	{ }

	#endregion

	private static string CreateFile(string parsed, string nameSpace)
		=> $$"""
			{{ HeaderTemplate}}

			namespace {{ nameSpace}};

			public partial class {{OutputPartialClassName}}
			{
			{{parsed}}
			}
			""";

	private static string Indent(int depth)
		=> new('\t', depth);

	private static string ParseSection(XElement element, string fileName, int depth, List<ParsedElement> parsedElements, ParsedElement? parentElement)
	{
		var name = depth == TopLevelDepth ? fileName : element.Name.LocalName;

		if (parsedElements.Any(x => x.Name == name && x.Depth == depth))
		{
			return string.Empty;
		}
		if (!element.HasElements)
		{
			var value = (parentElement?.FullPath + Delimiter + name).Replace($"{fileName}/", string.Empty);
			return $"{Indent(depth)}public const string {name} = \"{value}\";";
		}

		var nextDepth = depth + 1;

		var newParsedElement = new ParsedElement(name, depth, parentElement);
		parsedElements.Add(newParsedElement);

		var section = element.Elements().Select(element1 => ParseSection(element1, fileName, nextDepth, parsedElements, newParsedElement)).ToList();

		var innerContent = string.Join("\n", section.Where(x => x != string.Empty));
		if (parentElement is not null)
		{
			return 
				$$"""
				{{ Indent(depth)}}public class {{ name}}
				{{ Indent(depth)}}{
				{{ innerContent}}
				{{ Indent(depth)}}}
				""";
		}
		return innerContent;
	}

	private static string ParseXmlContent(SourceText content, string fileName, List<ParsedElement> parsedElements)
	{
		var parsed = XDocument.Parse(content.ToString());
		var languagesRoot = parsed.Root;
		var firstLanguage = languagesRoot?.Elements().FirstOrDefault();
		if (firstLanguage is null)
		{
			return string.Empty;
		}

		var recursivelyParsed = ParseSection(firstLanguage, fileName, TopLevelDepth, parsedElements, null);
		return CreateFile(recursivelyParsed, "LanguageFiles");
	}

	private static string SanitizePath(string filePath)
	{
		var indexOfLastBackslash = filePath.LastIndexOf('\\');
		return filePath.Substring(indexOfLastBackslash + 1);
	}

	private sealed class ParsedElement
	{
		public ParsedElement(string name, int depth, ParsedElement? parentElement)
		{
			Name = name;
			Depth = depth;
			ParentElement = parentElement;
		}

		public string Name { get; }
		public int Depth { get; }
		public ParsedElement? ParentElement { get; }
		public string FullPath => ParentElement is null ? Name : ParentElement.FullPath + Delimiter + Name;
	}
}
