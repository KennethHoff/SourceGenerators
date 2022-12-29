using System.Reflection;
using Oxx.Backend.Generators.PocoSchema.Core.Configuration.Events;
using Oxx.Backend.Generators.PocoSchema.Core.Models.Schemas.Contracts;
using Oxx.Backend.Generators.PocoSchema.Core.Models.Types;

namespace Oxx.Backend.Generators.PocoSchema.Core.Configuration.Abstractions;

public abstract class
	SchemaConfigurationBuilder<TSchemaType, TConfigurationType, TSchemaEventConfiguration> : ISchemaConfigurationBuilder<TSchemaType, TConfigurationType,
		TSchemaEventConfiguration>
	where TSchemaType : class, ISchema
	where TConfigurationType : ISchemaConfiguration<TSchemaType, TSchemaEventConfiguration>
	where TSchemaEventConfiguration : ISchemaEventConfiguration, new()
{
	protected readonly TypeSchemaDictionary<TSchemaType> AtomicSchemasToCreateDictionary = new();
	protected readonly TypeTypeDictionary GenericSchemasDictionary = new();
	protected TSchemaEventConfiguration? EventConfiguration;
	protected string OutputDirectory = string.Empty;

	protected IList<Assembly> Assemblies { get; } = new List<Assembly>();

	protected Action AtomicSchemaApplicationAction { get; private set; } = null!;
	protected abstract TConfigurationType Configuration { get; }

	protected bool DeleteFilesOnStart { get; set; }
	protected abstract string FileExtension { get; set; }
	protected abstract string FileNameFormat { get; set; }
	protected abstract string SchemaEnumNamingFormat { get; set; }

	protected abstract string SchemaNamingFormat { get; set; }
	protected abstract string SchemaTypeNamingFormat { get; set; }

	#region Interface implementations

	public SchemaConfigurationBuilder<TSchemaType, TConfigurationType, TSchemaEventConfiguration> ApplyAtomicSchema<TType, TSchema>(
		Func<TSchema>? schemaFactory = null)
		where TSchema : TSchemaType, IAtomicSchema, new()
	{
		UpsertSchemaTypeDictionary<TType, TSchema>(schemaFactory);
		return this;
	}

	public SchemaConfigurationBuilder<TSchemaType, TConfigurationType, TSchemaEventConfiguration> ApplyGenericSchema(
		Type genericType,
		Type genericSchema)
	{
		var genericTypeDefinition = genericType.GetTypeInfo().GetGenericTypeDefinition();
		var genericSchemasDefinition = genericSchema.GetTypeInfo().GetGenericTypeDefinition();
		UpsertGenericSchemaTypeDictionary(genericTypeDefinition, genericSchemasDefinition);
		return this;
	}

	public TConfigurationType Build()
	{
		AtomicSchemaApplicationAction();
		return Configuration;
	}

	public SchemaConfigurationBuilder<TSchemaType, TConfigurationType, TSchemaEventConfiguration> ConfigureEvents(Action<TSchemaEventConfiguration> action)
	{
		EventConfiguration = new TSchemaEventConfiguration();
		action(EventConfiguration);
		return this;
	}

	/// <summary>
	///     Be careful with this method, it will delete all files in the output directory
	/// </summary>
	public SchemaConfigurationBuilder<TSchemaType, TConfigurationType, TSchemaEventConfiguration> DeleteExistingFiles(bool shouldDelete = true)
	{
		DeleteFilesOnStart = shouldDelete;
		return this;
	}

	public SchemaConfigurationBuilder<TSchemaType, TConfigurationType, TSchemaEventConfiguration> OverrideFileExtension(string fileExtension)
	{
		FileExtension = fileExtension;
		return this;
	}

	public SchemaConfigurationBuilder<TSchemaType, TConfigurationType, TSchemaEventConfiguration> OverrideFileNameNamingFormat(string namingFormat)
	{
		FileNameFormat = namingFormat;
		return this;
	}

	public SchemaConfigurationBuilder<TSchemaType, TConfigurationType, TSchemaEventConfiguration> OverrideSchemaNamingFormat(string namingFormat)
	{
		SchemaNamingFormat = namingFormat;
		return this;
	}

	public SchemaConfigurationBuilder<TSchemaType, TConfigurationType, TSchemaEventConfiguration> OverrideSchemaTypeNamingFormat(string namingFormat)
	{
		SchemaTypeNamingFormat = namingFormat;
		return this;
	}

	public SchemaConfigurationBuilder<TSchemaType, TConfigurationType, TSchemaEventConfiguration> ResolveTypesFromAssemblyContaining<T>()
	{
		Assemblies.Add(typeof(T).Assembly);
		return this;
	}

	public SchemaConfigurationBuilder<TSchemaType, TConfigurationType, TSchemaEventConfiguration> SetRootDirectory(string rootDirectory)
	{
		OutputDirectory = rootDirectory;
		return this;
	}

	#endregion

	public SchemaConfigurationBuilder<TSchemaType, TConfigurationType, TSchemaEventConfiguration> OverrideSchemaEnumNamingFormat(string namingFormat)
	{
		SchemaEnumNamingFormat = namingFormat;
		return this;
	}

	protected void ApplyAtomicSchemas(Action action)
	{
		AtomicSchemaApplicationAction = action;
	}

	private void UpsertGenericSchemaTypeDictionary(Type genericType, Type genericSchema)
	{
		// Get the generic type definition of the generic type
		var genericTypeTypeInfo = genericType.GetTypeInfo();

		// Get the generic type definition of the generic schema
		var genericSchemaTypeInfo = genericSchema.GetTypeInfo();

		// Throw an exception if the generic schema does not implement TSchemaType
		if (genericSchemaTypeInfo.ImplementedInterfaces.Contains(typeof(TSchemaType)) is false)
		{
			throw new ArgumentException($"The generic schema {genericSchema} does not implement {typeof(TSchemaType)}");
		}

		// If the two generics don't have the same number of generic parameters, throw an exception
		if (genericTypeTypeInfo.GenericTypeParameters.Length != genericSchemaTypeInfo.GenericTypeParameters.Length)
		{
			throw new ArgumentException(
				$"The generic type {genericTypeTypeInfo.Name} and the generic schema {genericSchemaTypeInfo.Name} do not have the same number of generic parameters.");
		}

		// if the two generics don't have the same number of generic arguments, throw an exception
		if (genericTypeTypeInfo.GenericTypeArguments.Length != genericSchemaTypeInfo.GenericTypeArguments.Length)
		{
			throw new ArgumentException(
				$"The generic type {genericTypeTypeInfo.Name} and the generic schema {genericSchemaTypeInfo.Name} do not have the same number of generic arguments.");
		}

		if (GenericSchemasDictionary.ContainsKey(genericType))
		{
			GenericSchemasDictionary[genericType] = genericSchema;
		}
		else
		{
			GenericSchemasDictionary.Add(genericType, genericSchema);
		}
	}

	private void UpsertSchemaTypeDictionary<TType, TSchema>(Func<TSchema>? substituteFactory = null) where TSchema : TSchemaType, new()
	{
		var type = typeof(TType);

		if (AtomicSchemasToCreateDictionary.ContainsKey(type))
		{
			AtomicSchemasToCreateDictionary[type] = substituteFactory is null
				? new TSchema()
				: substituteFactory();
		}
		else
		{
			AtomicSchemasToCreateDictionary.Add(type, substituteFactory is null
				? new TSchema()
				: substituteFactory());
		}
	}

	private void UpsertSchemaTypeDictionary<TSchema>(Type genericType, Func<TSchema>? substituteFactory = null) where TSchema : TSchemaType, new()
	{
		if (AtomicSchemasToCreateDictionary.ContainsKey(genericType))
		{
			AtomicSchemasToCreateDictionary[genericType] = substituteFactory is null
				? new TSchema()
				: substituteFactory();
		}
		else
		{
			AtomicSchemasToCreateDictionary.Add(genericType, substituteFactory is null
				? new TSchema()
				: substituteFactory());
		}
	}
}
