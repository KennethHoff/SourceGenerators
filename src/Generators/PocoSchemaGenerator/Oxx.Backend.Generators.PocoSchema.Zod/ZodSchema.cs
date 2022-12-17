using System.Reflection;
using Oxx.Backend.Generators.PocoSchema.Zod.Core;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes;

namespace Oxx.Backend.Generators.PocoSchema.Zod;

public sealed class ZodSchema : ISchema
{
	private readonly IDictionary<Type, IZodSchemaType> _schemaTypes;
	private readonly string _outputDirectory;

	public ZodSchema(SchemaGeneratorConfigurationBuilder<IZodSchemaType> configurationBuilder)
	{
		_schemaTypes = configurationBuilder.SchemaTypeDictionary;
		_outputDirectory = configurationBuilder.OutputDirectory;
	}

	public SchemaOutput GenerateFileContent(PocoObject pocoObject)
		=> new($"{SchemaName(pocoObject)}.ts",
			$$"""
		import { z } from 'zod';

		{{GenerateSchema(pocoObject)}}

		{{GenerateType(pocoObject)}}
		""");

	private string GenerateSchema(PocoObject pocoObject)
		=> $$"""
		export const {{SchemaName(pocoObject)}} = z.object({
			{{GenerateProperties(pocoObject)}}
		});
		""";

	private string GenerateProperties(PocoObject pocoObject)
		=> pocoObject.Properties.Select(GenerateProperty).JoinWithNewLine();
	
	private string GenerateProperty(PropertyInfo property)
		=> $"{property.Name}: {GeneratePropertyType(property.PropertyType)},";

	private string GeneratePropertyType(Type type)
		=> _schemaTypes[type].ValidationSchemaLogic;
	
	private string SchemaName(PocoObject pocoObject)
		=> $"{pocoObject.Name}Schema";

	private string GenerateType(PocoObject pocoObject)
		=> $"export type {TypeName(pocoObject)} = z.infer<typeof {SchemaName(pocoObject)}>;";
	
	private string TypeName(PocoObject pocoObject)
		=> $"{SchemaName(pocoObject)}Type";

	public bool Generate(IEnumerable<PocoObject> pocoObjects)
	{
		foreach (var pocoObject in pocoObjects)
		{
			var schemaOutput = GenerateFileContent(pocoObject);
			var fullPath = Path.Combine(_outputDirectory, schemaOutput.FileName);
			File.WriteAllText(fullPath, schemaOutput.FileContent);
		}

		return true;
	}
}
