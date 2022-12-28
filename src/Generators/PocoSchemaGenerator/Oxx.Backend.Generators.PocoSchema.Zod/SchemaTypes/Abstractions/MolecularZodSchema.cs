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
	public IEnumerable<ZodImport> AdditionalImports => SchemaDictionary
		.Select(x => x.Value)
		.OfType<IAdditionalImportZodSchema>()
		.SelectMany(x => x.AdditionalImports.Where(import => import != ZodImport.None))
		.Concat(SchemaDictionary
			.Select(x => x.Value)
			.Where(x => x is not IBuiltInAtomicZodSchema and not IAdditionalImportZodSchema)
			.Select(SchemaConfiguration.CreateStandardImport))
		.Distinct();

	public SchemaBaseName SchemaBaseName { get; private init; }

	public SchemaDefinition SchemaDefinition => new($$"""
		z.object({
		{{SchemaContent}}
		})
		""");

	public IDictionary<PropertyInfo, IPartialZodSchema> SchemaDictionary { get; private init; } = new Dictionary<PropertyInfo, IPartialZodSchema>();
	private ZodSchemaConfiguration SchemaConfiguration { get; init; } = null!;

	private string SchemaContent => SchemaDictionary
		.Aggregate(string.Empty, (a, b)
			=>
		{
			var propertyName = b.Key.Name.ToCamelCaseInvariant();
			var propertySchema = SchemaConfiguration.FormatSchemaName(b.Value);

			if (b.Key.IsNullable())
			{
				// If the property is nullable, we need to make the property allow null (still required, and undefined is not allowed)
				propertySchema += ".nullable()";
			}

			return $"{a}\t{propertyName}: {propertySchema},{Environment.NewLine}";
		})
		.TrimEnd($"{Environment.NewLine}");

	private MolecularZodSchema()
	{ }

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
}
