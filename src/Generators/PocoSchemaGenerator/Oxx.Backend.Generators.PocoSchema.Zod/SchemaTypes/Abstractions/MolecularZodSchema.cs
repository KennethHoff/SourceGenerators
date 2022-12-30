using Namotion.Reflection;
using Oxx.Backend.Generators.PocoSchema.Core.Extensions;
using Oxx.Backend.Generators.PocoSchema.Core.Models.Types;
using Oxx.Backend.Generators.PocoSchema.Zod.Configuration;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts.Models;

namespace Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Abstractions;

// Before fully generating the molecule, we need to generate the definitions for all molecules in order to be able to reference them
// This PartialMolecularZodSchema is used as a stepping stone in this process
public class PartialMolecularZodSchema : IPartialZodSchema
{
	public SchemaBaseName SchemaBaseName { get; init; }

	public MolecularZodSchema Populate(IDictionary<SchemaMemberInfo, IPartialZodSchema> schemaDictionary, ZodSchemaConfiguration schemaConfiguration)
		=> MolecularZodSchema.CreateFromPartial(this, schemaDictionary, schemaConfiguration);
}

public class MolecularZodSchema : IMolecularZodSchema
{
	public IEnumerable<ZodImport> AdditionalImports
	{
		get
		{
			
			var standardImports = SchemaDictionary
				.Select(x => x.Value)
				.Where(x => x is not IBuiltInAtomicZodSchema)
				.Select(SchemaConfiguration.CreateStandardImport)
				.ToArray();

			var additionalImportZodSchemata = SchemaDictionary
				.Select(x => x.Value)
				.Where(x => x is not IMolecularZodSchema)
				.OfType<IAdditionalImportZodSchema>()
				.ToArray();

			var additionalImports = additionalImportZodSchemata
				.SelectMany(x => x.AdditionalImports)
				.ToArray();

			var distinct = standardImports.Concat(additionalImports)
				.Distinct()
				.ToArray();
			return distinct;
		}
	}

	public SchemaBaseName SchemaBaseName { get; private init; }

	public SchemaDefinition SchemaDefinition => new($$"""
		z.object({
		{{SchemaContent}}
		})
		""");

	public IDictionary<SchemaMemberInfo, IPartialZodSchema> SchemaDictionary { get; private init; } = new Dictionary<SchemaMemberInfo, IPartialZodSchema>();
	private ZodSchemaConfiguration SchemaConfiguration { get; init; } = null!;

	private string SchemaContent => SchemaDictionary
		.Aggregate(string.Empty, (a, b)
			=>
		{
			var propertyName = b.Key.Name.ToCamelCaseInvariant();
			var propertySchema = SchemaConfiguration.FormatSchemaName(b.Value);

			if (b.Key.ContextualType.Nullability is Nullability.Nullable)
			{
				// If the property is nullable, we need to make the property allow null (still required, and undefined is not allowed)
				propertySchema += ".nullable()";
			}

			return $"{a}\t{propertyName}: {propertySchema},{Environment.NewLine}";
		})
		.TrimEnd($"{Environment.NewLine}");

	public static MolecularZodSchema CreateFromPartial(
		PartialMolecularZodSchema partial,
		IDictionary<SchemaMemberInfo, IPartialZodSchema> schemaDictionary,
		ZodSchemaConfiguration zodSchemaConfiguration)
		=> new()
		{
			SchemaDictionary = schemaDictionary,
			SchemaBaseName = partial.SchemaBaseName,
			SchemaConfiguration = zodSchemaConfiguration,
		};
}
