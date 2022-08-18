﻿using System.Collections.Immutable;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Microsoft.CodeAnalysis.Text;

namespace LanguageFileParser.Generator;

[Generator]
public class LanguageFileTransformGenerator : ISourceGenerator
{
	private const string Authors = "Kenneth Hoff";

	private static readonly Assembly ThisAssembly = typeof(LanguageFileTransformGenerator).Assembly;
	private static readonly Version AssemblyVersion = ThisAssembly.GetName().Version;

	#region Interface implementations

	public void Execute(GeneratorExecutionContext context)
	{
		// find anything that matches our files
		// var myFiles = context.AdditionalFiles.Where(x => x.Path.Contains(".xml")).ToList();
		var myFiles = context.AdditionalFiles
			.Where(at => Regex.IsMatch(at.Path, @"^.*Resources(\\|/)LanguageFiles(\\|/).*.xml$"))
			.ToImmutableList();

		myFiles.ForEach(file =>
		{
			var content = file.GetText(context.CancellationToken);
			if (content is null)
			{
				return;
			}

			var parsed = ParseXmlContent(content);
			var sanitizedPath = SanitizePath(file.Path);
			context.AddSource($"{sanitizedPath}.g.cs", parsed);
		});
	}

	public void Initialize(GeneratorInitializationContext context)
	{ }

	#endregion

	private static string CreateFile(XDocument parsed)
		=> $"""
			// This file is generated by LanguageFileTransformGenerator.
			// Do not edit this file directly, instead edit the source file and regenerate this file.
			// Version: { AssemblyVersion}   
			// Authors: { Authors}   

			{ parsed}   
			""" ;

	private static string ParseXmlContent(SourceText content)
	{
		var parsed = XDocument.Parse(content.ToString());
		return CreateFile(parsed);
	}

	private static string SanitizePath(string filePath)
	{
		var indexOfLastBackslash = filePath.LastIndexOf('\\');
		return filePath.Substring(indexOfLastBackslash + 1);
	}
}
