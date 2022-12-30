using Oxx.Backend.Generators.PocoSchema.Core.Configuration.Events;

namespace Oxx.Backend.Generators.PocoSchema.Zod.Configuration;

public sealed class ZodSchemaEvents : ISchemaEvents
{
	public EventHandler<FileCreatedEventArgs>? FileCreated { get; set; }
	public EventHandler<FileCreatingEventArgs>? FileCreating { get; set; }
	public EventHandler<FilesCreatedEventArgs>? FilesCreated { get; set; }
	public EventHandler<FilesCreatingEventArgs>? FilesCreating { get; set; }
	public EventHandler<PocoStructuresCreatedEventArgs>? PocoStructuresCreated { get; set; }
	public EventHandler<GenerationStartedEventArgs>? GenerationStarted { get; set; }
	public EventHandler<GenerationCompletedEventArgs>? GenerationCompleted { get; set; }
	public EventHandler<DeletingFilesEventArgs>? DeletingFiles { get; set; }
	public EventHandler<DeletingFilesFailedEventArgs>? DeletingFilesFailed { get; set; }
	public EventHandler<MoleculeSchemaCreatedEventArgs>? MoleculeSchemaCreated { get; set; }
	public EventHandler<MoleculeSchemasCreatedEventArgs>? MoleculeSchemasCreated { get; set; }
}
