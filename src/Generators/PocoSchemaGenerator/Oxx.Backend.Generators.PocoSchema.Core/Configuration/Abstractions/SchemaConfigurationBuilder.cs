using System.Reflection;
using Oxx.Backend.Generators.PocoSchema.Core.Configuration.Events;
using Oxx.Backend.Generators.PocoSchema.Core.Models.Contracts;

namespace Oxx.Backend.Generators.PocoSchema.Core.Configuration.Abstractions;

public abstract class SchemaConfigurationBuilder<TSchemaType, TConfigurationType, TSchemaEventConfiguration> : ISchemaConfigurationBuilder<TConfigurationType>
	where TSchemaType : class, IAtomicSchema
	where TConfigurationType : ISchemaConfiguration<TSchemaType, TSchemaEventConfiguration>
	where TSchemaEventConfiguration : ISchemaEventConfiguration, new()
{
	protected readonly IDictionary<Type, TSchemaType> SchemaTypeDictionary = new Dictionary<Type, TSchemaType>();
	protected string OutputDirectory = string.Empty;

	protected abstract string SchemaNamingFormat { get; set; }
	protected abstract string SchemaTypeNamingFormat { get; set; }
	protected abstract string SchemaFileNameFormat { get; set; }
	protected TSchemaEventConfiguration? EventConfiguration;

	public IList<Assembly> Assemblies { get; } = new List<Assembly>();
	internal bool IsValid => !string.IsNullOrWhiteSpace(OutputDirectory) && Assemblies.Any();

	protected bool DeleteFilesOnStart { get; set; } = true;

	#region Interface implementations

	public TConfigurationType Build()
		=> CreateConfiguration();

	#endregion

	/// <summary>
	/// Be careful with this method, it will delete all files in the output directory
	/// </summary>
	public SchemaConfigurationBuilder<TSchemaType, TConfigurationType,TSchemaEventConfiguration> DeleteExistingFiles(bool shouldDelete = true)
	{
		DeleteFilesOnStart = shouldDelete;
		return this;
	}

	public SchemaConfigurationBuilder<TSchemaType, TConfigurationType,TSchemaEventConfiguration> OverrideSchemaNamingFormat(string namingFormat)
	{
		SchemaNamingFormat = namingFormat;
		return this;
	}

	public SchemaConfigurationBuilder<TSchemaType, TConfigurationType,TSchemaEventConfiguration> OverrideSchemaTypeNamingFormat(string namingFormat)
	{
		SchemaTypeNamingFormat = namingFormat;
		return this;
	}

	public SchemaConfigurationBuilder<TSchemaType, TConfigurationType,TSchemaEventConfiguration> ResolveTypesFromAssemblies(params Assembly[] assemblies)
	{
		foreach (var assembly in assemblies)
		{
			ResolveTypesFromAssembly(assembly);
		}

		return this;
	}

	public SchemaConfigurationBuilder<TSchemaType, TConfigurationType,TSchemaEventConfiguration> ResolveTypesFromAssembly(Assembly assembly)
	{
		Assemblies.Add(assembly);
		return this;
	}

	public SchemaConfigurationBuilder<TSchemaType, TConfigurationType,TSchemaEventConfiguration> ResolveTypesFromAssemblyContaining<T>()
	{
		ResolveTypesFromAssembly(typeof(T).GetTypeInfo().Assembly);
		return this;
	}

	public SchemaConfigurationBuilder<TSchemaType, TConfigurationType,TSchemaEventConfiguration> SetRootDirectory(string rootDirectory)
	{
		OutputDirectory = rootDirectory;
		return this;
	}

	protected SchemaConfigurationBuilder<TSchemaType, TConfigurationType,TSchemaEventConfiguration> Substitute<TType, TSubstitute>() where TType : class
		where TSubstitute : TSchemaType, new()
	{
		UpsertSchemaTypeDictionary<TType, TSubstitute>();
		return this;
	}

	protected SchemaConfigurationBuilder<TSchemaType, TConfigurationType,TSchemaEventConfiguration> SubstituteIncludingNullable<TType, TSubstitute>() where TType : struct
		where TSubstitute : TSchemaType, new()
	{
		UpsertSchemaTypeDictionary<TType, TSubstitute>();
		UpsertSchemaTypeDictionary<TType?, TSubstitute>();
		return this;
	}

	public SchemaConfigurationBuilder<TSchemaType, TConfigurationType,TSchemaEventConfiguration> SubstituteExcludingNullable<TType, TSubstitute>()
		where TSubstitute: TSchemaType, new()
	{
		UpsertSchemaTypeDictionary<TType, TSubstitute>();
		return this;
	}
	protected abstract TConfigurationType CreateConfiguration();

	private void UpsertSchemaTypeDictionary<TType, TSubstitute>() where TSubstitute : TSchemaType, new()
	{
		var type = typeof(TType);
		if (SchemaTypeDictionary.ContainsKey(type))
		{
			SchemaTypeDictionary[type] = new TSubstitute();
		}
		else
		{
			SchemaTypeDictionary.Add(type, new TSubstitute());
		}
	}

	public SchemaConfigurationBuilder<TSchemaType, TConfigurationType,TSchemaEventConfiguration> ConfigureEvents(Action<TSchemaEventConfiguration> action)
	{
		EventConfiguration = new TSchemaEventConfiguration();
		action(EventConfiguration);
		
		
		return this;
	}
}