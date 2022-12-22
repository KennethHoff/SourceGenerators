using System.Reflection;
using Oxx.Backend.Generators.PocoSchema.Core.Extensions;
using Oxx.Backend.Generators.PocoSchema.Zod.Configuration;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts.Models;

namespace Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Abstractions;

// Before fully generating the molecule, we need to generate the definitions for all molecules in order to be able to reference them
// This PartialMolecularZodSchema is used as a stepping stone in this process
public class PartialMolecularZodSchema : IPartialZodSchema
{
	public SchemaBaseName SchemaBaseName { get; init; }

	public MolecularZodSchema Populate(IDictionary<PropertyInfo, IPartialZodSchema> schemaDictionary, ZodSchemaConfiguration schemaConfiguration)
		=> MolecularZodSchema.CreateFromPartial(this, schemaDictionary, schemaConfiguration);
}

public class MolecularZodSchema : IMolecularZodSchema
{
	private MolecularZodSchema()
	{ }

	public SchemaBaseName SchemaBaseName { get; private init; }
	private ZodSchemaConfiguration SchemaConfiguration { get; init; } = null!; 

	public static MolecularZodSchema CreateFromPartial(
		PartialMolecularZodSchema partial,
		IDictionary<PropertyInfo, IPartialZodSchema> schemaDictionary,
		ZodSchemaConfiguration zodSchemaConfiguration)
		=> new()
		{
			SchemaDictionary = schemaDictionary,
			SchemaBaseName = partial.SchemaBaseName,
			SchemaConfiguration = zodSchemaConfiguration,
		};

	public SchemaDefinition SchemaDefinition => new($$"""
		z.object({
		{{SchemaContent}}
		})
		""");

	private string SchemaContent => SchemaDictionary
		.Aggregate(string.Empty, (a, b) 
			=> $"{a}\t{b.Key.Name.ToCamelCaseInvariant()}: {SchemaConfiguration.FormatSchemaName(b.Value)},{Environment.NewLine}")
		.TrimEnd($"{Environment.NewLine}");

	public string AdditionalImports => SchemaDictionary
		.Select(x => x.Value)
		.Where(x => x is not IBuiltInAtomicZodSchema)
		.Select(x => new
		{
			FilePath = SchemaConfiguration.FormatFilePath(x),
			SchemaName = SchemaConfiguration.FormatSchemaName(x),
		})
		.Distinct()
		.Aggregate(string.Empty, (a, b)
			=> $$"""
			{{a}}import { {{b.SchemaName}} } from "{{b.FilePath}}";{{Environment.NewLine}}
			""")
		.TrimEnd(Environment.NewLine);

	public IDictionary<PropertyInfo, IPartialZodSchema> SchemaDictionary { get; private init; } = new Dictionary<PropertyInfo, IPartialZodSchema>();
}
