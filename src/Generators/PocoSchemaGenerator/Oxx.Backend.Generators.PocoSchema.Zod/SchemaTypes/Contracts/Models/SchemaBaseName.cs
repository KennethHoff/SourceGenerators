namespace Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;

public readonly record struct SchemaBaseName(string Value)
{
	// Implicit conversion from SchemaBaseName to string
	public static implicit operator string(SchemaBaseName schemaBaseName) => schemaBaseName.Value;
	
	public override string ToString()
		=> Value;
}
