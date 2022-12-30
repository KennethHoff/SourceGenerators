using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Oxx.Backend.Generators.PocoSchema.Core.Configuration.Events;
using Oxx.Backend.Generators.PocoSchema.Core.Models.Schemas.Contracts;
using Oxx.Backend.Generators.PocoSchema.Core.Models.Types;

namespace Oxx.Backend.Generators.PocoSchema.Core.Configuration.Abstractions;

public abstract class SchemaConfigurationBuilder<TSchemaType, TConfigurationType, TSchemaEventConfiguration> : 
	ISchemaConfigurationBuilder<SchemaConfigurationBuilder<TSchemaType, TConfigurationType, TSchemaEventConfiguration>, 
		TSchemaType, TConfigurationType, TSchemaEventConfiguration>
	where TSchemaType : class, ISchema
	where TConfigurationType : ISchemaConfiguration<TSchemaType, TSchemaEventConfiguration>
	where TSchemaEventConfiguration : ISchemaEventConfiguration, new()
{
	protected readonly TypeSchemaDictionary<TSchemaType> AtomicSchemasToCreateDictionary = new();
	protected readonly TypeTypeDictionary GenericSchemasDictionary = new();
	protected TSchemaEventConfiguration? EventConfiguration;
	protected DirectoryInfo OutputDirectory = null!;

	protected IList<Assembly> Assemblies { get; } = new List<Assembly>();
	protected Action AtomicSchemaApplicationAction { get; private set; } = null!;
	protected abstract TConfigurationType Configuration { get; }
	protected FileDeletionMode FileDeletionMode { get; set; }
	protected abstract string FileExtension { get; set; }
	protected abstract string FileExtensionInfix { get; set; }
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

	public SchemaConfigurationBuilder<TSchemaType, TConfigurationType, TSchemaEventConfiguration> OverrideFileDeletionMode(FileDeletionMode fileDeletionMode)
	{
		FileDeletionMode = fileDeletionMode;
		return this;
	}

	public SchemaConfigurationBuilder<TSchemaType, TConfigurationType, TSchemaEventConfiguration> OverrideFileExtensionInfix(string infix)
	{
		FileExtensionInfix = infix;
		return this;
	}

	public SchemaConfigurationBuilder<TSchemaType, TConfigurationType, TSchemaEventConfiguration> OverrideFileNameNamingFormat(string format)
	{
		FileNameFormat = EnsureValidFormat(format);
		return this;
	}

	public SchemaConfigurationBuilder<TSchemaType, TConfigurationType, TSchemaEventConfiguration> OverrideSchemaEnumNamingFormat(string format)
	{
		SchemaEnumNamingFormat = EnsureValidFormat(format);
		;
		return this;
	}

	public SchemaConfigurationBuilder<TSchemaType, TConfigurationType, TSchemaEventConfiguration> OverrideSchemaNamingFormat(string format)
	{
		SchemaNamingFormat = EnsureValidFormat(format);
		return this;
	}

	public SchemaConfigurationBuilder<TSchemaType, TConfigurationType, TSchemaEventConfiguration> OverrideSchemaTypeNamingFormat(string format)
	{
		SchemaTypeNamingFormat = EnsureValidFormat(format);
		return this;
	}

	public SchemaConfigurationBuilder<TSchemaType, TConfigurationType, TSchemaEventConfiguration> ResolveTypesFromAssemblyContaining<T>()
	{
		if (Assemblies.Contains(typeof(T).Assembly))
		{
			throw new InvalidOperationException($"Assembly {typeof(T).Assembly.FullName} is already added");
		}

		Assemblies.Add(typeof(T).Assembly);
		return this;
	}

	public SchemaConfigurationBuilder<TSchemaType, TConfigurationType, TSchemaEventConfiguration> SetRootDirectory(string rootDirectory, [CallerFilePath] string callerFilePath = "")
	{
		var callerDirectory = Path.GetDirectoryName(callerFilePath);
		OutputDirectory = new DirectoryInfo(Path.Combine(callerDirectory!, rootDirectory));

		if (OutputDirectory.Parent?.Exists is not true)
		{
			throw new InvalidOperationException($"Directory {OutputDirectory.Parent?.FullName} does not exist");
		}
		return this;
	}

	#endregion

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

	private static string EnsureValidFormat(string format)
	{
		var regex = new Regex(@"\{[\d]+\}", RegexOptions.Compiled);
		var exceptions = new List<Exception>();
		if (!format.Contains("{0}"))
		{
			exceptions.Add(new ArgumentException("The format must contain a {0} placeholder"));
		}

		if (regex.IsMatch(format))
		{
			exceptions.Add(new ArgumentException("The format must not contain any other placeholders than {0}"));
		}

		if (exceptions.Any())
		{
			throw new AggregateException(exceptions);
		}

		return format;
	}
}
