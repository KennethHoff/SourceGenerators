using Oxx.Backend.Generators.PocoSchema.Core.Models;

namespace Oxx.Backend.Generators.PocoSchema.Core.Configuration.Events;

public sealed class FilesCreatedEventArgs : EventArgs
{
	public IReadOnlyCollection<FileInformation> FileInformations { get; }

	public FilesCreatedEventArgs(IReadOnlyCollection<FileInformation> fileInformations)
	{
		FileInformations = fileInformations;
	}
}
