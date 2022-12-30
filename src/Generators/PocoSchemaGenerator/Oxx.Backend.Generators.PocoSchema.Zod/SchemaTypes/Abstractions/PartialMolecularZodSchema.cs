using Oxx.Backend.Generators.PocoSchema.Core.Models.Types;
using Oxx.Backend.Generators.PocoSchema.Zod.Configuration;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts.Models;

namespace Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Abstractions;

public class PartialMolecularZodSchema : IPartialZodSchema
{
	public SchemaBaseName SchemaBaseName { get; init; }

	public MolecularZodSchema Populate(IDictionary<SchemaMemberInfo, IPartialZodSchema> schemaDictionary, ZodSchemaConfiguration schemaConfiguration)
		=> MolecularZodSchema.CreateFromPartial(this, schemaDictionary, schemaConfiguration);
}