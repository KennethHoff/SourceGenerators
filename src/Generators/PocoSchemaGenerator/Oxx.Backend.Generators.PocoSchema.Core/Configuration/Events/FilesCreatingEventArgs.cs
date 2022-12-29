using Oxx.Backend.Generators.PocoSchema.Core.Models.File;

namespace Oxx.Backend.Generators.PocoSchema.Core.Configuration.Events;

public sealed class FilesCreatingEventArgs : EventArgs
{
	public ICollection<FileInformation> FileInformations { get; }

	public FilesCreatingEventArgs(ICollection<FileInformation> fileInformations)
	{
		FileInformations = fileInformations;
	}
}
