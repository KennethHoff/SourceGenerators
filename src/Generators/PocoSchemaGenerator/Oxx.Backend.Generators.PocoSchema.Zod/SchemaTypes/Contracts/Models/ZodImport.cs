namespace Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts.Models;

public readonly record struct ZodImport
{
	public static readonly ZodImport None = new()
	{
		SchemaName = string.Empty,
		FilePath = string.Empty,
	};

	private readonly string _filePath;

	public required string SchemaName { get; init; }

	public required string FilePath
	{
		get => _filePath;
		init => _filePath = value.Replace("\\", "/");
	}

	#region Overrides

	public override string ToString()
		=> $$"""import { {{SchemaName}} } from "{{FilePath}}";""";

	#endregion
}