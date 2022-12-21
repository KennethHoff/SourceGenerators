using Oxx.Backend.Generators.PocoSchema.Core.Models;

namespace Oxx.Backend.Generators.PocoSchema.Core.Configuration.Events;

public sealed class FileCreatingEventArgs : EventArgs
{
	public readonly FileInformation SchemaInformation;

	public FileCreatingEventArgs(FileInformation schemaInformation)
	{
		SchemaInformation = schemaInformation;
	}

	public bool Skip { get; set; }
}