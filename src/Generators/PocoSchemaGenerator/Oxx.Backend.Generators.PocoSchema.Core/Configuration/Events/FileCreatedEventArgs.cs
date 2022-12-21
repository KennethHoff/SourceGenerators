using Oxx.Backend.Generators.PocoSchema.Core.Models;

namespace Oxx.Backend.Generators.PocoSchema.Core.Configuration.Events;

public sealed class FileCreatedEventArgs : EventArgs
{
	public readonly FileInformation SchemaInformation;

	public FileCreatedEventArgs(FileInformation schemaInformation)
	{
		SchemaInformation = schemaInformation;
	}

	public required bool Skipped { get; init; }
}