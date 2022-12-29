using Oxx.Backend.Generators.PocoSchema.Core.Models.File;

namespace Oxx.Backend.Generators.PocoSchema.Core.Configuration.Events;

public sealed class FileCreatedEventArgs : EventArgs
{
	public readonly FileInformation SchemaInformation;

	public required bool Skipped { get; init; }

	public FileCreatedEventArgs(FileInformation schemaInformation)
	{
		SchemaInformation = schemaInformation;
	}
}
