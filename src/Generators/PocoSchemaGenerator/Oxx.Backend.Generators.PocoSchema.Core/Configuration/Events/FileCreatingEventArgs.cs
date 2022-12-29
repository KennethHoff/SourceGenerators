using Oxx.Backend.Generators.PocoSchema.Core.Models.File;

namespace Oxx.Backend.Generators.PocoSchema.Core.Configuration.Events;

public sealed class FileCreatingEventArgs : EventArgs
{
	public readonly FileInformation SchemaInformation;

	public bool Skip { get; set; }

	public FileCreatingEventArgs(FileInformation schemaInformation)
	{
		SchemaInformation = schemaInformation;
	}
}
