namespace Oxx.Backend.Generators.PocoSchema.Core.Models.Files;

public record class FileInformation
{
	public static readonly FileInformation None = new()
	{
		OutputDirectory = null!,
		Name = string.Empty,
		Content = FileContent.None,
		Type = null!,
	};

	public required string Name { get; set; }
	public required FileContent Content { get; set; }
	public required DirectoryInfo OutputDirectory { get; set; }
	public required Type Type { get; set; }
}
