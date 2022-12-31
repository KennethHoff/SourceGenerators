namespace Oxx.Backend.Generators.PocoSchema.Core.Models.Files;

public readonly record struct FileInformation
{
	public static readonly FileInformation None = new()
	{
		OutputDirectory = null!,
		Name = string.Empty,
		Content = FileContent.None,
	};

	public required string Name { get; init; }
	public required FileContent Content { get; init; }
	public required DirectoryInfo OutputDirectory { get; init; }
}
