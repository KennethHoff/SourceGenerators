namespace Oxx.Backend.Generators.PocoSchema.Core.Configuration.Events;

public sealed class DeletingFilesFailedEventArgs : EventArgs
{
	public DeletingFilesFailedEventArgs(DirectoryInfo directory, IReadOnlyCollection<FileInfo> files, Exception exception)
	{
		Directory = directory;
		Files = files;
		Exception = exception;
	}

	public DirectoryInfo Directory { get; }
	public IReadOnlyCollection<FileInfo> Files { get; }
	public Exception Exception { get; }
}
