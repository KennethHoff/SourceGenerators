using System.Reflection;
using Oxx.Backend.Generators.PocoSchema.Core.Extensions;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;

namespace Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Abstractions;

// Before fully generating the molecule, we need to generate the definitions for all molecules in order to be able to reference them
// This PartialMolecularZodSchema is used as a stepping stone in this process
public class PartialMolecularZodSchema : IPartialZodSchema
{
	public SchemaBaseName SchemaBaseName { get; init; }

	public MolecularZodSchema Populate(IDictionary<PropertyInfo, IZodSchema> schemaDictionary)
		=> MolecularZodSchema.CreateFromPartial(this, schemaDictionary);
}

public class MolecularZodSchema : IMolecularZodSchema
{
	private MolecularZodSchema()
	{ }
	public SchemaBaseName SchemaBaseName { get; private init; }

	public static MolecularZodSchema CreateFromPartial(PartialMolecularZodSchema partial, IDictionary<PropertyInfo, IZodSchema> schemaDictionary)
		=> new()
		{
			SchemaDictionary = schemaDictionary,
			SchemaBaseName = partial.SchemaBaseName,
		};

	public SchemaDefinition SchemaDefinition => new($$"""
		z.object({
		{{SchemaContent}}
		})
		""");

	private string SchemaContent => SchemaDictionary
		.Aggregate(string.Empty, (a, b) 
			=> $"{a}\t{b.Key.Name.ToCamelCaseInvariant()}: {b.Value.SchemaBaseName},\n")
		.TrimEnd(',', '\n');

	public string AdditionalImports => """
	import { noob } from "Daniel"
	""";

	public IDictionary<PropertyInfo, IZodSchema> SchemaDictionary { get; private init; } = new Dictionary<PropertyInfo, IZodSchema>();
}
