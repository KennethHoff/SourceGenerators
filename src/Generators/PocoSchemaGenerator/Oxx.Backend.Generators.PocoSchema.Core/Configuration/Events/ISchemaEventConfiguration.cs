namespace Oxx.Backend.Generators.PocoSchema.Core.Configuration.Events;

public interface ISchemaEventConfiguration
{
	public EventHandler<FileCreatingEventArgs>? FileCreating { get; set; }
	public EventHandler<FileCreatedEventArgs>? FileCreated { get; set; }
	public EventHandler<FilesCreatingEventArgs>? FilesCreating { get; set; }
	public EventHandler<FilesCreatedEventArgs>? FilesCreated { get; set; }
}