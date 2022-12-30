using Oxx.Backend.Generators.PocoSchema.Core.Models.Files;

namespace Oxx.Backend.Generators.PocoSchema.Core.Configuration.Events;

public sealed class FilesCreatingEventArgs : EventArgs
{
	public IReadOnlyCollection<FileInformation> FileInformations { get; }

	public FilesCreatingEventArgs(IReadOnlyCollection<FileInformation> fileInformations)
	{
		FileInformations = fileInformations;
	}
}
