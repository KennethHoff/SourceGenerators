namespace Oxx.Backend.Generators.PocoSchema.Core.Models;

public readonly record struct FileInformation(FileName Name, FileContent Content)
{
	public static readonly FileInformation None = new(FileName.None, FileContent.None);
}
