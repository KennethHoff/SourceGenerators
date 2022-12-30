namespace Oxx.Backend.Generators.PocoSchema.Core.Configuration.Events;

public sealed class DeletingFilesEventArgs : EventArgs
{
	public DeletingFilesEventArgs(DirectoryInfo directory, IReadOnlyCollection<FileInfo> files)
	{
		Directory = directory;
		Files = files;
	}

	public DirectoryInfo Directory { get; }
	public IReadOnlyCollection<FileInfo> Files { get; }
}