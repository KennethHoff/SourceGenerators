using Oxx.Backend.Generators.PocoSchema.Core.Configuration;
using Oxx.Backend.Generators.PocoSchema.Core.Configuration.Events;
using Oxx.Backend.Generators.PocoSchema.Core.Contracts;
using Oxx.Backend.Generators.PocoSchema.Core.Logic.FileCreation;
using Oxx.Backend.Generators.PocoSchema.Core.Logic.PocoExtraction;

namespace Oxx.Backend.Generators.PocoSchema.Core;

public abstract class SchemaGenerator<TSchemaEvents> : ISchemaGenerator
	where TSchemaEvents : ISchemaEvents
{
	private readonly ISchemaConfiguration<TSchemaEvents> _configuration;
	private readonly ISchemaConverter _schemaConverter;
	private readonly IPocoStructureExtractor _pocoStructureExtractor;
	private readonly ISchemaFileCreator _fileCreator;

	protected SchemaGenerator(ISchemaConfiguration<TSchemaEvents> configuration, ISchemaConverter schemaConverter, IPocoStructureExtractor pocoStructureExtractor, ISchemaFileCreator fileCreator)
	{
		_schemaConverter = schemaConverter;
		_configuration = configuration;
		_pocoStructureExtractor = pocoStructureExtractor;
		_fileCreator = fileCreator;
	}

	public async Task GenerateAllAsync()
	{
		var generationStartedTime = DateTime.Now;
		_configuration.Events.GenerationStarted?.Invoke(this, new GenerationStartedEventArgs(generationStartedTime));

		var pocoStructures = _pocoStructureExtractor.GetAllFromAssemblies(_configuration.Assemblies);
		var fileInformations = _schemaConverter.GenerateFileContent(pocoStructures).ToList();
		
		await _fileCreator.CreateFilesAsync(fileInformations);

		var generationCompletedTime = DateTime.Now;
		_configuration.Events.GenerationCompleted?.Invoke(this, new GenerationCompletedEventArgs(generationStartedTime, generationCompletedTime));
	}

	public Task GenerateAsync<TPoco>()
		=> GenerateAsync(typeof(TPoco));

	public Task GenerateAsync(Type pocoType)
		=> GenerateAsync(new[] { pocoType });

	public async Task GenerateAsync(IEnumerable<Type> pocoTypes)
	{
		var generationStartedTime = DateTime.Now;
		_configuration.Events.GenerationStarted?.Invoke(this, new GenerationStartedEventArgs(generationStartedTime));

		var pocoStructures = _pocoStructureExtractor.Get(pocoTypes);
		var fileInformations = _schemaConverter.GenerateFileContent(pocoStructures).ToList();

		await _fileCreator.CreateFilesAsync(fileInformations);

		var generationCompletedTime = DateTime.Now;
		_configuration.Events.GenerationCompleted?.Invoke(this, new GenerationCompletedEventArgs(generationStartedTime, generationCompletedTime));
	}
}