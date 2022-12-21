using Oxx.Backend.Generators.PocoSchema.Core.Models;

namespace Oxx.Backend.Generators.PocoSchema.Core.Configuration.Events;

public sealed class SchemaCreatingEventArgs : EventArgs
{
	public SchemaCreatingEventArgs(FileInformation schemaInformation)
	{
		SchemaInformation = schemaInformation;
	}

	public FileInformation SchemaInformation { get; }
}