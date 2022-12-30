namespace Oxx.Backend.Generators.PocoSchema.Core.Exceptions;

public sealed class DirectoryContainsManuallyCreatedFilesException : Exception
{
	public DirectoryContainsManuallyCreatedFilesException(IReadOnlyCollection<FileInfo> manuallyCreatedFiles)
	{
		ManuallyCreatedFiles = manuallyCreatedFiles;
	}

	public IReadOnlyCollection<FileInfo> ManuallyCreatedFiles { get; }
}

