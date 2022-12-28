using Oxx.Backend.Generators.PocoSchema.Core.Configuration.Events;

namespace Oxx.Backend.Generators.PocoSchema.Zod.Configuration;

public sealed class ZodSchemaEventConfiguration : ISchemaEventConfiguration
{
	public EventHandler<FileCreatedEventArgs>? FileCreated { get; set; }
	public EventHandler<FileCreatingEventArgs>? FileCreating { get; set; }
	public EventHandler<FilesCreatedEventArgs>? FilesCreated { get; set; }
	public EventHandler<FilesCreatingEventArgs>? FilesCreating { get; set; }
}
