namespace Oxx.Backend.Generators.PocoSchema.Core.Configuration.Events;

public interface ISchemaEventConfiguration
{
	EventHandler<FileCreatedEventArgs>? FileCreated { get; set; }
	EventHandler<FileCreatingEventArgs>? FileCreating { get; set; }
	EventHandler<FilesCreatedEventArgs>? FilesCreated { get; set; }
	EventHandler<FilesCreatingEventArgs>? FilesCreating { get; set; }
	EventHandler<PocoStructuresCreatedEventArgs>? PocoStructuresCreated { get; set; }
	EventHandler<GenerationStartedEventArgs>? GenerationStarted { get; set; }
	EventHandler<GenerationCompletedEventArgs>? GenerationCompleted { get; set; }
	EventHandler<DeletingFilesEventArgs>? DeletingFiles { get; set; }
	EventHandler<DeletingFilesFailedEventArgs>? DeletingFilesFailed { get; set; }
}
