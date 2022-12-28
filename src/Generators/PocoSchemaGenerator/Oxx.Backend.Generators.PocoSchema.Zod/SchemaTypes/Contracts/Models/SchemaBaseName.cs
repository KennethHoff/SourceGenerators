using Oxx.Backend.Generators.PocoSchema.Core.Extensions;

namespace Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts.Models;

public readonly record struct SchemaBaseName(string Value)
{
	#region Overrides

	public override string ToString()
		=> Value.ToCamelCaseInvariant();

	#endregion

	// Implicit conversion from SchemaBaseName to string
	public static implicit operator string(SchemaBaseName schemaBaseName)
		=> schemaBaseName.ToString();
}
