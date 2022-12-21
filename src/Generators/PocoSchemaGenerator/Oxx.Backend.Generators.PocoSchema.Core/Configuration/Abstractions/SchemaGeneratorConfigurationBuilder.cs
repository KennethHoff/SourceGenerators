using System.Reflection;

namespace Oxx.Backend.Generators.PocoSchema.Core.Configuration.Abstractions;

public abstract class SchemaGeneratorConfigurationBuilder<TSchemaType, TConfigurationType> : ISchemaGeneratorConfigurationBuilder<TConfigurationType>
	where TSchemaType : class, ISchemaType
	where TConfigurationType : ISchemaGeneratorConfiguration
{
	public readonly IDictionary<Type, TSchemaType> SchemaTypeDictionary = new Dictionary<Type, TSchemaType>();
	protected string OutputDirectory = string.Empty;

	protected string SchemaNamingConvention = "{0}Schema";
	protected string SchemaTypeNamingConvention = "{0}SchemaType";

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
	public SchemaGeneratorConfigurationBuilder<TSchemaType, TConfigurationType> DeleteExistingFiles(bool shouldDelete = true)
	{
		DeleteFilesOnStart = shouldDelete;
		return this;
	}

	public SchemaGeneratorConfigurationBuilder<TSchemaType, TConfigurationType> OverrideSchemaNamingConvention(string namingFormat)
	{
		SchemaNamingConvention = namingFormat;
		return this;
	}

	public SchemaGeneratorConfigurationBuilder<TSchemaType, TConfigurationType> OverrideSchemaTypeNamingConvention(string namingFormat)
	{
		SchemaTypeNamingConvention = namingFormat;
		return this;
	}

	public SchemaGeneratorConfigurationBuilder<TSchemaType, TConfigurationType> ResolveTypesFromAssemblies(params Assembly[] assemblies)
	{
		foreach (var assembly in assemblies)
		{
			ResolveTypesFromAssembly(assembly);
		}

		return this;
	}

	public SchemaGeneratorConfigurationBuilder<TSchemaType, TConfigurationType> ResolveTypesFromAssembly(Assembly assembly)
	{
		Assemblies.Add(assembly);
		return this;
	}

	public SchemaGeneratorConfigurationBuilder<TSchemaType, TConfigurationType> ResolveTypesFromAssemblyContaining<T>()
	{
		ResolveTypesFromAssembly(typeof(T).GetTypeInfo().Assembly);
		return this;
	}

	public SchemaGeneratorConfigurationBuilder<TSchemaType, TConfigurationType> SetRootDirectory(string rootDirectory)
	{
		OutputDirectory = rootDirectory;
		return this;
	}

	protected SchemaGeneratorConfigurationBuilder<TSchemaType, TConfigurationType> Substitute<TType, TSubstitute>() where TType : class
		where TSubstitute : TSchemaType, new()
	{
		UpsertSchemaTypeDictionary<TType, TSubstitute>();
		return this;
	}

	protected SchemaGeneratorConfigurationBuilder<TSchemaType, TConfigurationType> SubstituteIncludingNullable<TType, TSubstitute>() where TType : struct
		where TSubstitute : TSchemaType, new()
	{
		UpsertSchemaTypeDictionary<TType, TSubstitute>();
		UpsertSchemaTypeDictionary<TType?, TSubstitute>();
		return this;
	}

	public SchemaGeneratorConfigurationBuilder<TSchemaType, TConfigurationType> SubstituteExcludingNullable<TType, TSubstitute>()
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
}

