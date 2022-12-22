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
	protected abstract string FileNameFormat { get; set; }
	protected TSchemaEventConfiguration? EventConfiguration;

	protected IList<Assembly> Assemblies { get; } = new List<Assembly>();

	protected bool DeleteFilesOnStart { get; set; }
	protected abstract string FileExtension { get; set; }

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
	
	public SchemaConfigurationBuilder<TSchemaType, TConfigurationType,TSchemaEventConfiguration> OverrideFileNameNamingFormat(string namingFormat)
	{
		FileNameFormat = namingFormat;
		return this;
	}
	
	public SchemaConfigurationBuilder<TSchemaType, TConfigurationType,TSchemaEventConfiguration> OverrideFileExtension(string fileExtension)
	{
		FileExtension = fileExtension;
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

	public SchemaConfigurationBuilder<TSchemaType, TConfigurationType,TSchemaEventConfiguration> ApplySchemaToStruct<TType, TSchema>(
		Func<TSchema>? schemaFactory = null)
		where TSchema : TSchemaType, new()
		where TType: struct
	{
		UpsertSchemaTypeDictionary<TType, TSchema>(schemaFactory);
		UpsertSchemaTypeDictionary<TType?, TSchema>(schemaFactory);
		return this;
	}
	
	public SchemaConfigurationBuilder<TSchemaType, TConfigurationType,TSchemaEventConfiguration> ApplySchemaToClass<TType, TSchema>(
		Func<TSchema>? schemaFactory = null)
		where TSchema : TSchemaType, new()
		where TType: class
	{
		UpsertSchemaTypeDictionary<TType, TSchema>(schemaFactory);
		return this;
	}
	protected abstract TConfigurationType CreateConfiguration();

	private void UpsertSchemaTypeDictionary<TType, TSchema>(Func<TSchema>? substituteFactory = null) where TSchema : TSchemaType, new()
	{
		var type = typeof(TType);
		
		if (SchemaTypeDictionary.ContainsKey(type))
		{
			SchemaTypeDictionary[type] = substituteFactory is null
				? new TSchema()
				: substituteFactory();
		}
		else
		{
			SchemaTypeDictionary.Add(type, substituteFactory is null
				? new TSchema()
				: substituteFactory());
		}
	}

	public SchemaConfigurationBuilder<TSchemaType, TConfigurationType,TSchemaEventConfiguration> ConfigureEvents(Action<TSchemaEventConfiguration> action)
	{
		EventConfiguration = new TSchemaEventConfiguration();
		action(EventConfiguration);
		return this;
	}
}