using Oxx.Backend.Generators.PocoSchema.Core.Models.Files;

namespace Oxx.Backend.Generators.PocoSchema.Core.Configuration.Events;

public sealed class FileCreatingEventArgs : EventArgs
{
	public FileInformation SchemaInformation { get; }

	public bool Skip { get; set; }

	public FileCreatingEventArgs(FileInformation schemaInformation)
	{
		SchemaInformation = schemaInformation;
	}
}
