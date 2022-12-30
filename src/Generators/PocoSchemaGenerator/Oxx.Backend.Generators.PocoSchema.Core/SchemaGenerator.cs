using Oxx.Backend.Generators.PocoSchema.Core.Configuration;
using Oxx.Backend.Generators.PocoSchema.Core.Configuration.Events;
using Oxx.Backend.Generators.PocoSchema.Core.Contracts;
using Oxx.Backend.Generators.PocoSchema.Core.Models.Schemas.Contracts;
using Oxx.Backend.Generators.PocoSchema.Core.PocoExtractors;

namespace Oxx.Backend.Generators.PocoSchema.Core;

public abstract class SchemaGenerator<TSchemaType, TSchemaEventConfiguration> : ISchemaGenerator
	where TSchemaType : ISchema
	where TSchemaEventConfiguration : ISchemaEventConfiguration
{
	private readonly ISchemaConfiguration<TSchemaType, TSchemaEventConfiguration> _configuration;
	private readonly ISchemaConverter _schemaConverter;
	private readonly IPocoStructureExtractor _pocoStructureExtractor;
	private readonly ISchemaFileCreator _fileCreator;

	protected SchemaGenerator(ISchemaConverter schemaConverter, ISchemaConfiguration<TSchemaType, TSchemaEventConfiguration> configuration, IPocoStructureExtractor pocoStructureExtractor, ISchemaFileCreator fileCreator)
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

		var pocoStructures = _pocoStructureExtractor.GetAll();
		var fileInformations = _schemaConverter.GenerateFileContent(pocoStructures).ToList();
		
		await _fileCreator.CreateFilesAsync(fileInformations);

		var generationCompletedTime = DateTime.Now;
		_configuration.Events.GenerationCompleted?.Invoke(this, new GenerationCompletedEventArgs(generationStartedTime, generationCompletedTime));
	}

	public Task GenerateAsync<TPoco>()
		=> throw new NotImplementedException();

	public Task GenerateAsync(Type pocoType)
		=> throw new NotImplementedException();
}