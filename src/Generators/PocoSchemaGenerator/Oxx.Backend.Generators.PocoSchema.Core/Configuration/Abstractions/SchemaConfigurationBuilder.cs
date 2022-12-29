using System.Reflection;
using Oxx.Backend.Generators.PocoSchema.Core.Configuration.Events;
using Oxx.Backend.Generators.PocoSchema.Core.Models.Schema.Contracts;
using Oxx.Backend.Generators.PocoSchema.Core.Models.Type;

namespace Oxx.Backend.Generators.PocoSchema.Core.Configuration.Abstractions;

public abstract class
	SchemaConfigurationBuilder<TSchemaType, TConfigurationType, TSchemaEventConfiguration> : ISchemaConfigurationBuilder<TSchemaType, TConfigurationType,
		TSchemaEventConfiguration>
	where TSchemaType : class, ISchema
	where TConfigurationType : ISchemaConfiguration<TSchemaType, TSchemaEventConfiguration>
	where TSchemaEventConfiguration : ISchemaEventConfiguration, new()
{
	protected readonly TypeTypeDictionary GenericSchemasDictionary = new();
	protected readonly TypeSchemaDictionary<TSchemaType> AtomicSchemasToCreateDictionary = new();
	protected TSchemaEventConfiguration? EventConfiguration;
	protected string OutputDirectory = string.Empty;

	protected IList<Assembly> Assemblies { get; } = new List<Assembly>();
	protected abstract TConfigurationType Configuration { get; }

	protected bool DeleteFilesOnStart { get; set; }
	protected abstract string FileExtension { get; set; }
	protected abstract string FileNameFormat { get; set; }

	protected Action SchemaApplicationAction { get; private set; } = null!;

	protected abstract string SchemaNamingFormat { get; set; }
	protected abstract string SchemaTypeNamingFormat { get; set; }
	protected abstract string SchemaEnumNamingFormat { get; set; }

	#region Interface implementations

	public SchemaConfigurationBuilder<TSchemaType, TConfigurationType, TSchemaEventConfiguration> ApplyGenericSchema(
		Type genericType,
		Type genericSchema)
	{
		var genericTypeDefinition = genericType.GetTypeInfo().GetGenericTypeDefinition();
		var genericSchemasDefinition = genericSchema.GetTypeInfo().GetGenericTypeDefinition();
		UpsertGenericSchemaTypeDictionary(genericTypeDefinition, genericSchemasDefinition);
		return this;
	}

	public SchemaConfigurationBuilder<TSchemaType, TConfigurationType, TSchemaEventConfiguration> ApplySchema<TType, TSchema>(
		Func<TSchema>? schemaFactory = null)
		where TSchema : TSchemaType, new()
	{
		UpsertSchemaTypeDictionary<TType, TSchema>(schemaFactory);

		// Add to Nullable<TType> if TType is a value type.
		// Unlike Reference Types, when ValueTypes are null, they are an entirely different type.
		// Specifically, Nullable<T> is a different type than T.
		// Reference Types are still the same type when they are null.
		if (typeof(TType).GetTypeInfo().IsValueType is false)
		{
			return this;
		}

		var isNullable = typeof(TType).GetTypeInfo().IsGenericType && typeof(TType).GetGenericTypeDefinition() == typeof(Nullable<>);
		if (isNullable)
		{
			return this;
		}

		var nullableType = typeof(Nullable<>).MakeGenericType(typeof(TType));
		UpsertSchemaTypeDictionary(nullableType, schemaFactory);
		return this;
	}

	public TConfigurationType Build()
	{
		SchemaApplicationAction();
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
	
	public SchemaConfigurationBuilder<TSchemaType, TConfigurationType, TSchemaEventConfiguration> OverrideSchemaEnumNamingFormat(string namingFormat)
	{
		SchemaEnumNamingFormat = namingFormat;
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

	protected void ApplySchemas(Action action)
	{
		SchemaApplicationAction = action;
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
