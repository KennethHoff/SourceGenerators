using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Oxx.Backend.Generators.PocoSchema.Core.Configuration.Events;
using Oxx.Backend.Generators.PocoSchema.Core.Models.Schemas.Contracts;
using Oxx.Backend.Generators.PocoSchema.Core.Models.Types;

namespace Oxx.Backend.Generators.PocoSchema.Core.Configuration.Abstractions;

public abstract class SchemaConfigurationBuilder<TSchema, TSchemaConfiguration, TSchemaEvents> : 
	ISchemaConfigurationBuilder<SchemaConfigurationBuilder<TSchema, TSchemaConfiguration, TSchemaEvents>, 
		TSchema, TSchemaConfiguration, TSchemaEvents>
	where TSchema : class, ISchema
	where TSchemaConfiguration : ISchemaConfiguration<TSchemaEvents>
	where TSchemaEvents : ISchemaEvents, new()
{
	protected readonly TypeSchemaDictionary<TSchema> AtomicSchemasToCreateDictionary = new();
	protected readonly TypeTypeDictionary GenericSchemasDictionary = new();
	protected TSchemaEvents? EventConfiguration;
	protected DirectoryInfo OutputDirectory = null!;

	protected List<Assembly> Assemblies { get; } = new List<Assembly>();
	protected Action AtomicSchemaApplicationAction { get; private set; } = null!;
	protected abstract TSchemaConfiguration Configuration { get; }
	protected FileDeletionMode FileDeletionMode { get; set; }
	protected abstract string FileExtension { get; set; }
	protected abstract string FileExtensionInfix { get; set; }
	protected abstract string FileNameFormat { get; set; }
	protected abstract string SchemaEnumNamingFormat { get; set; }
	protected abstract string SchemaNamingFormat { get; set; }
	protected abstract string SchemaTypeNamingFormat { get; set; }

	#region Interface implementations

	public SchemaConfigurationBuilder<TSchema, TSchemaConfiguration, TSchemaEvents> ApplyAtomicSchema<TType, TAtomicSchema>(
		Func<TAtomicSchema>? schemaFactory = null)
		where TAtomicSchema : TSchema, IAtomicSchema, new()
	{
		UpsertSchemaTypeDictionary<TType, TAtomicSchema>(schemaFactory);
		return this;
	}

	public SchemaConfigurationBuilder<TSchema, TSchemaConfiguration, TSchemaEvents> ApplyGenericSchema(
		Type genericType,
		Type genericSchema)
	{
		var genericTypeDefinition = genericType.GetTypeInfo().GetGenericTypeDefinition();
		var genericSchemasDefinition = genericSchema.GetTypeInfo().GetGenericTypeDefinition();
		UpsertGenericSchemaTypeDictionary(genericTypeDefinition, genericSchemasDefinition);
		return this;
	}

	public TSchemaConfiguration Build()
	{
		if (OutputDirectory is null)
		{
			throw new InvalidOperationException("Output directory is not set.");
		}
		AtomicSchemaApplicationAction();
		return Configuration;
	}

	public SchemaConfigurationBuilder<TSchema, TSchemaConfiguration, TSchemaEvents> ConfigureEvents(Action<TSchemaEvents> action)
	{
		EventConfiguration = new TSchemaEvents();
		action(EventConfiguration);
		return this;
	}

	public SchemaConfigurationBuilder<TSchema, TSchemaConfiguration, TSchemaEvents> OverrideFileDeletionMode(FileDeletionMode fileDeletionMode)
	{
		FileDeletionMode = fileDeletionMode;
		return this;
	}

	public SchemaConfigurationBuilder<TSchema, TSchemaConfiguration, TSchemaEvents> OverrideFileExtensionInfix(string infix)
	{
		FileExtensionInfix = infix;
		return this;
	}

	public SchemaConfigurationBuilder<TSchema, TSchemaConfiguration, TSchemaEvents> OverrideFileNameNamingFormat(string format)
	{
		FileNameFormat = EnsureValidFormat(format);
		return this;
	}

	public SchemaConfigurationBuilder<TSchema, TSchemaConfiguration, TSchemaEvents> OverrideSchemaEnumNamingFormat(string format)
	{
		SchemaEnumNamingFormat = EnsureValidFormat(format);
		;
		return this;
	}

	public SchemaConfigurationBuilder<TSchema, TSchemaConfiguration, TSchemaEvents> OverrideSchemaNamingFormat(string format)
	{
		SchemaNamingFormat = EnsureValidFormat(format);
		return this;
	}

	public SchemaConfigurationBuilder<TSchema, TSchemaConfiguration, TSchemaEvents> OverrideSchemaTypeNamingFormat(string format)
	{
		SchemaTypeNamingFormat = EnsureValidFormat(format);
		return this;
	}

	public SchemaConfigurationBuilder<TSchema, TSchemaConfiguration, TSchemaEvents> ResolveTypesFromAssemblyContaining<T>()
	{
		if (Assemblies.Contains(typeof(T).Assembly))
		{
			throw new InvalidOperationException($"Assembly {typeof(T).Assembly.FullName} is already added");
		}

		Assemblies.Add(typeof(T).Assembly);
		return this;
	}

	public SchemaConfigurationBuilder<TSchema, TSchemaConfiguration, TSchemaEvents> SetRootDirectory(string rootDirectory, [CallerFilePath] string callerFilePath = "")
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
		if (genericSchemaTypeInfo.ImplementedInterfaces.Contains(typeof(TSchema)) is false)
		{
			throw new ArgumentException($"The generic schema {genericSchema} does not implement {typeof(TSchema)}");
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

	private void UpsertSchemaTypeDictionary<TType, TSchemaToUpsert>(Func<TSchemaToUpsert>? substituteFactory = null) 
		where TSchemaToUpsert : TSchema, new()
	{
		var type = typeof(TType);

		if (AtomicSchemasToCreateDictionary.ContainsKey(type))
		{
			AtomicSchemasToCreateDictionary[type] = substituteFactory is null
				? new TSchemaToUpsert()
				: substituteFactory();
		}
		else
		{
			AtomicSchemasToCreateDictionary.Add(type, substituteFactory is null
				? new TSchemaToUpsert()
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
