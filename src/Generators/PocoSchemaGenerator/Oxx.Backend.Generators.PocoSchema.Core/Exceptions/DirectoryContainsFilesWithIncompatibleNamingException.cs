namespace Oxx.Backend.Generators.PocoSchema.Core.Exceptions;

public sealed class DirectoryContainsFilesWithIncompatibleNamingException : Exception
{
	public DirectoryContainsFilesWithIncompatibleNamingException(IReadOnlyCollection<FileInfo> filesWithInvalidFileExtensions)
	{
		FilesWithInvalidFileExtensions = filesWithInvalidFileExtensions;
	}

	public IReadOnlyCollection<FileInfo> FilesWithInvalidFileExtensions { get; }
}

