namespace Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts.Models;

public readonly record struct ZodImport(string SchemaName, string FilePath)
{
	public static readonly ZodImport None = new(string.Empty, string.Empty);

	#region Overrides

	public override string ToString()
		=> $$"""import { {{SchemaName}} } from "{{FilePath}}";""";

	#endregion
}