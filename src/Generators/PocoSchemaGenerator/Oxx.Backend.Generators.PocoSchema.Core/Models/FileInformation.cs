namespace Oxx.Backend.Generators.PocoSchema.Core.Models;

public readonly record struct FileInformation
{
	public static readonly FileInformation None = new()
	{
		Content = FileContent.None,
		Name = FileName.None,
	};

	public required FileName Name { get; init; }
	public required FileContent Content { get; init; }
}