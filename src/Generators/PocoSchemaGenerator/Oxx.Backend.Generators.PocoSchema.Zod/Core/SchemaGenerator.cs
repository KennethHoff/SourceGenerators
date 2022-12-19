using System.Reflection;
using Microsoft.CodeAnalysis;

namespace Oxx.Backend.Generators.PocoSchema.Zod.Core;

public sealed class SchemaGenerator
{
	private readonly ISchema _schema;
	private readonly IList<Assembly> _assemblies;
	private readonly string _outputDirectory;

	public SchemaGenerator(ISchema schema, SchemaGeneratorConfigurationBuilder configurationBuilder)
	{
		_schema = schema;
		_assemblies = configurationBuilder.Assemblies;
		_outputDirectory = configurationBuilder.OutputDirectory;
	}

	public IEnumerable<FileInformation> CreateFiles(SemanticModel semanticModel, IEnumerable<PocoObject> pocoObjects)
	{
		var contents = _schema.GenerateFileContent(pocoObjects).ToArray();
		
		foreach (var (fileName, fileContent) in contents)
		{
			var filePath = Path.Combine(_outputDirectory, fileName);
			File.WriteAllText(filePath, fileContent);
		}

		return contents;
	}
}