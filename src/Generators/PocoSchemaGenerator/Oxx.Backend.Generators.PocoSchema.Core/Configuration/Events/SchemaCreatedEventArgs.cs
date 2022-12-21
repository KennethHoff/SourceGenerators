using Oxx.Backend.Generators.PocoSchema.Core.Models;

namespace Oxx.Backend.Generators.PocoSchema.Core.Configuration.Events;

public sealed class SchemaCreatedEventArgs : EventArgs
{
	public SchemaCreatedEventArgs(FileInformation schemaInformation)
	{
		SchemaInformation = schemaInformation;
	}

	public FileInformation SchemaInformation { get; }
}