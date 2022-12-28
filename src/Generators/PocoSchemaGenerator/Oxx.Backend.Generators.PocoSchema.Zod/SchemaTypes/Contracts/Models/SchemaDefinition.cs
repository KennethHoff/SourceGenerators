namespace Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts.Models;

public readonly record struct SchemaDefinition(string Value)
{
	#region Overrides

	public override string ToString()
		=> Value;

	#endregion

	// Implicit conversion from SchemaDefinition to string
	public static implicit operator string(SchemaDefinition schemaDefinition)
		=> schemaDefinition.Value;
}
