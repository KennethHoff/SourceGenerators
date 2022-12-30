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
	private readonly ISchemaFileCreator _fileCreator;
	private readonly IPocoStructureExtractor _pocoStructureExtractor;
	private readonly ISchemaConverter _schemaConverter;

	private bool _alreadyGenerated;

	protected SchemaGenerator(ISchemaConfiguration<TSchemaEvents> configuration, ISchemaConverter schemaConverter, IPocoStructureExtractor pocoStructureExtractor, ISchemaFileCreator fileCreator)
	{
		_schemaConverter = schemaConverter;
		_configuration = configuration;
		_pocoStructureExtractor = pocoStructureExtractor;
		_fileCreator = fileCreator;
	}

	#region Interface implementations

	public async Task GenerateAllAsync()
	{
		EnsureNotAlreadyGenerated();

		var generationStartedTime = DateTime.Now;
		_configuration.Events.GenerationStarted?.Invoke(this, new GenerationStartedEventArgs(generationStartedTime));

		var pocoStructures = _pocoStructureExtractor.GetAll();
		var fileInformations = _schemaConverter.GenerateFileContent(pocoStructures).ToArray();

		await _fileCreator.CreateFilesAsync(fileInformations);

		var generationCompletedTime = DateTime.Now;
		_configuration.Events.GenerationCompleted?.Invoke(this, new GenerationCompletedEventArgs(generationStartedTime, generationCompletedTime));
	}

	public Task GenerateAsync<TPoco>(bool includeDependencies = true)
		=> GenerateAsync(typeof(TPoco), includeDependencies);

	public Task GenerateAsync(Type pocoType, bool includeDependencies = true)
		=> GenerateAsync(new[]
		{
			pocoType,
		}, includeDependencies);

	public async Task GenerateAsync(IEnumerable<Type> pocoTypes, bool includeDependencies = true)
	{
		EnsureNotAlreadyGenerated();

		var generationStartedTime = DateTime.Now;
		_configuration.Events.GenerationStarted?.Invoke(this, new GenerationStartedEventArgs(generationStartedTime));

		var pocoStructures = _pocoStructureExtractor.Get(pocoTypes, includeDependencies);
		var fileInformations = _schemaConverter.GenerateFileContent(pocoStructures).ToArray();

		await _fileCreator.CreateFilesAsync(fileInformations);

		var generationCompletedTime = DateTime.Now;
		_configuration.Events.GenerationCompleted?.Invoke(this, new GenerationCompletedEventArgs(generationStartedTime, generationCompletedTime));
	}

	#endregion
	
	private void EnsureNotAlreadyGenerated()
	{
		if (_alreadyGenerated)
		{
			throw new InvalidOperationException("Schema generation has already been performed. Create a new instance of the generator to perform another generation.");
		}

		_alreadyGenerated = true;
	}
}