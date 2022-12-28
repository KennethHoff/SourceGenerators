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

	public IEnumerable<ZodImport> AdditionalImports
	{
		get
		{
			var selectMany = SchemaDictionary
				.Select(x => x.Value)
				.OfType<IAdditionalImportZodSchema>()
				.SelectMany(x => x.AdditionalImports.Where(import => import != ZodImport.None));
			var zodImports = SchemaDictionary
				.Select(x => x.Value)
				.Where(x => x is not IBuiltInZodSchema and not IAdditionalImportZodSchema)
				.Select(SchemaConfiguration.CreateStandardImport);
			return selectMany.Concat(zodImports)
				.Distinct();
		}
	}

	public IDictionary<PropertyInfo, IPartialZodSchema> SchemaDictionary { get; private init; } = new Dictionary<PropertyInfo, IPartialZodSchema>();
}