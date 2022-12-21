namespace Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;

public readonly record struct SchemaDefinition(string Value)
{
	// Implicit conversion from SchemaDefinition to string
	public static implicit operator string(SchemaDefinition schemaDefinition) => schemaDefinition.Value;

	public override string ToString()
		=> Value;
}
