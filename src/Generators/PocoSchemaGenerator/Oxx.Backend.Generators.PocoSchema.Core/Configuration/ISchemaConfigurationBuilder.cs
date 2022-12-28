using System.Reflection;
using Oxx.Backend.Generators.PocoSchema.Core.Configuration.Abstractions;
using Oxx.Backend.Generators.PocoSchema.Core.Configuration.Events;
using Oxx.Backend.Generators.PocoSchema.Core.Models.Contracts;

namespace Oxx.Backend.Generators.PocoSchema.Core.Configuration;

public interface ISchemaConfigurationBuilder<TSchemaType, TConfigurationType, TSchemaEventConfiguration> where TSchemaType : class, ISchema where TConfigurationType : ISchemaConfiguration<TSchemaType, TSchemaEventConfiguration> where TSchemaEventConfiguration : ISchemaEventConfiguration, new()
{
	TConfigurationType Build();

	/// <summary>
	/// Be careful with this method, it will delete all files in the output directory
	/// </summary>
	SchemaConfigurationBuilder<TSchemaType, TConfigurationType,TSchemaEventConfiguration> SetRootDirectory(string rootDirectory);
	SchemaConfigurationBuilder<TSchemaType, TConfigurationType,TSchemaEventConfiguration> DeleteExistingFiles(bool shouldDelete = true);
	SchemaConfigurationBuilder<TSchemaType, TConfigurationType,TSchemaEventConfiguration> OverrideSchemaNamingFormat(string namingFormat);
	SchemaConfigurationBuilder<TSchemaType, TConfigurationType,TSchemaEventConfiguration> OverrideSchemaTypeNamingFormat(string namingFormat);
	SchemaConfigurationBuilder<TSchemaType, TConfigurationType,TSchemaEventConfiguration> OverrideFileNameNamingFormat(string namingFormat);
	SchemaConfigurationBuilder<TSchemaType, TConfigurationType,TSchemaEventConfiguration> OverrideFileExtension(string fileExtension);
	SchemaConfigurationBuilder<TSchemaType, TConfigurationType,TSchemaEventConfiguration> ApplySchema<TType, TSchema>(
		Func<TSchema>? schemaFactory = null)
		where TSchema : TSchemaType, new();
	SchemaConfigurationBuilder<TSchemaType, TConfigurationType, TSchemaEventConfiguration> ApplyGenericSchema(
		Type genericType,
		Type genericSchema);
	SchemaConfigurationBuilder<TSchemaType, TConfigurationType,TSchemaEventConfiguration> ConfigureEvents(Action<TSchemaEventConfiguration> action);
	SchemaConfigurationBuilder<TSchemaType, TConfigurationType,TSchemaEventConfiguration> ResolveTypesFromAssemblyContaining<TType>();
}
