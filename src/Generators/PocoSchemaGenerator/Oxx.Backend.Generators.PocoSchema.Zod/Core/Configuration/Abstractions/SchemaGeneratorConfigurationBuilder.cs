using System.Reflection;

namespace Oxx.Backend.Generators.PocoSchema.Zod.Core.Configuration.Abstractions;

public abstract class SchemaGeneratorConfigurationBuilder<TSchemaType, TConfigurationType> : ISchemaGeneratorConfigurationBuilder<TConfigurationType>
	where TSchemaType : class, ISchemaType
	where TConfigurationType : ISchemaGeneratorConfiguration
{
	internal readonly IDictionary<Type, TSchemaType> SchemaTypeDictionary = new Dictionary<Type, TSchemaType>();
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

	public SchemaGeneratorConfigurationBuilder<TSchemaType, TConfigurationType> DeleteExistingFiles(bool shouldDelete = true)
	{
		DeleteFilesOnStart = shouldDelete;
		return this;
	}

	/// <summary>
	/// {0} = Type Name <br/>
	/// Default is `{}Schema`
	/// </summary>
	public SchemaGeneratorConfigurationBuilder<TSchemaType, TConfigurationType> OverrideSchemaNamingConvention(string namingFormat)
	{
		SchemaNamingConvention = namingFormat;
		return this;
	}

	/// <summary>
	/// {0} = Type Name <br/>
	/// Default is `{0}SchemaType
	/// </summary>
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

	public abstract void Substitute<TPoco, TSubstitute>() where TSubstitute : TSchemaType, new();

	protected abstract TConfigurationType CreateConfiguration();
}

