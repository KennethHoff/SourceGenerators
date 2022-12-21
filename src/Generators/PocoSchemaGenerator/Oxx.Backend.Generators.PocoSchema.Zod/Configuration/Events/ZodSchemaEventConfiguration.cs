using Oxx.Backend.Generators.PocoSchema.Core.Configuration.Events;

namespace Oxx.Backend.Generators.PocoSchema.Zod.Configuration.Events;

public sealed class ZodSchemaEventConfiguration : ISchemaEventConfiguration
{
	public EventHandler<SchemaCreatingEventArgs>? SchemaCreating { get; set; }
	public EventHandler<SchemaCreatedEventArgs>? SchemaCreated { get; set; }
	public EventHandler<FileCreatingEventArgs>? FileCreating { get; set; }
	public EventHandler<FileCreatedEventArgs>? FileCreated { get; set; }
	public EventHandler<FilesCreatingEventArgs>? FilesCreating { get; set; }
	public EventHandler<FilesCreatedEventArgs>? FilesCreated { get; set; }
}

