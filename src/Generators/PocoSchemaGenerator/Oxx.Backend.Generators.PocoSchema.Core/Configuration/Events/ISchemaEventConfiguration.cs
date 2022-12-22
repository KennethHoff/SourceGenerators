namespace Oxx.Backend.Generators.PocoSchema.Core.Configuration.Events;

public interface ISchemaEventConfiguration
{
	EventHandler<FileCreatingEventArgs>? FileCreating { get; set; }
	EventHandler<FileCreatedEventArgs>? FileCreated { get; set; }
	EventHandler<FilesCreatingEventArgs>? FilesCreating { get; set; }
	EventHandler<FilesCreatedEventArgs>? FilesCreated { get; set; }
}