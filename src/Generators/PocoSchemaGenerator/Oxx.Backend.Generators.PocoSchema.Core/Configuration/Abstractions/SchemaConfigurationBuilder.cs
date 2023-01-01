using System.Reflection;
using System.Text.RegularExpressions;
using Oxx.Backend.Generators.PocoSchema.Core.Configuration.Events;
using Oxx.Backend.Generators.PocoSchema.Core.Models.Schemas.Contracts;
using Oxx.Backend.Generators.PocoSchema.Core.Models.Types;

namespace Oxx.Backend.Generators.PocoSchema.Core.Configuration.Abstractions;

public abstract partial class SchemaConfigurationBuilder<TSelf, TSchema, TAtomicSchema, TSchemaConfiguration, TSchemaEvents, TDirectoryOutputConfiguration> 
	: ISchemaConfigurationBuilder<TSelf, TSchema, TAtomicSchema, TSchemaConfiguration, TSchemaEvents, TDirectoryOutputConfiguration>
	where TSelf : SchemaConfigurationBuilder<TSelf, TSchema, TAtomicSchema, TSchemaConfiguration, TSchemaEvents, TDirectoryOutputConfiguration>
	where TSchema : class, ISchema
	where TAtomicSchema: class, TSchema, IAtomicSchema
	where TSchemaConfiguration : ISchemaConfiguration<TSchemaEvents, TDirectoryOutputConfiguration>
	where TDirectoryOutputConfiguration : IDirectoryOutputConfiguration, new()
	where TSchemaEvents : ISchemaEvents, new()
{
	protected readonly TypeTypeDictionary GenericSchemasDictionary = new();
	protected TSchemaEvents? EventConfiguration;
	public TDirectoryOutputConfiguration DirectoryOutputConfiguration { get; set; } = new();

	protected ICollection<Assembly> Assemblies { get; } = new List<Assembly>();
	public TypeSchemaDictionary<TAtomicSchema> AtomDictionary { get; set; } = new();
	protected abstract TSchemaConfiguration Configuration { get; }
	protected FileDeletionMode FileDeletionMode { get; set; }
	protected abstract string FileExtension { get; set; }
	protected abstract string FileExtensionInfix { get; set; }
	protected abstract string FileNameFormat { get; set; }
	protected abstract string SchemaEnumNamingFormat { get; set; }
	protected abstract string SchemaNamingFormat { get; set; }
	protected abstract string SchemaTypeNamingFormat { get; set; }

	#region Interface implementations

	public TSelf ApplyAtomicSchema<TType, TAppliedSchema>(
		Func<TAppliedSchema>? schemaFactory = null)
		where TAppliedSchema : TAtomicSchema, new()
	{
		UpsertSchemaTypeDictionary<TType, TAppliedSchema>(schemaFactory);
		return (TSelf)this;
	}

	public TSelf ApplyGenericSchema(
		Type genericType,
		Type genericSchema)
	{
		var genericTypeDefinition = genericType.GetTypeInfo().GetGenericTypeDefinition();
		var genericSchemasDefinition = genericSchema.GetTypeInfo().GetGenericTypeDefinition();
		UpsertGenericSchemaTypeDictionary(genericTypeDefinition, genericSchemasDefinition);
		return (TSelf)this;
	}

	public TSchemaConfiguration Build()
	{
		EnsureValidConfiguration();
		return Configuration;
	}

	protected abstract void EnsureValidConfiguration();

	public TSelf ConfigureEvents(Action<TSchemaEvents> action)
	{
		EventConfiguration = new TSchemaEvents();
		action(EventConfiguration);
		return (TSelf)this;
	}

	public TSelf OverrideFileDeletionMode(FileDeletionMode fileDeletionMode)
	{
		FileDeletionMode = fileDeletionMode;
		return (TSelf)this;
	}

	public TSelf OverrideFileExtensionInfix(string infix)
	{
		FileExtensionInfix = infix;
		return (TSelf)this;
	}

	public TSelf OverrideFileNameNamingFormat(string format)
	{
		FileNameFormat = EnsureValidFormat(format, "FileNameFormat");
		return (TSelf)this;
	}

	public TSelf OverrideSchemaEnumNamingFormat(string format)
	{
		SchemaEnumNamingFormat = EnsureValidFormat(format, "SchemaEnumNamingFormat");
		return (TSelf)this;
	}

	public TSelf OverrideSchemaNamingFormat(string format)
	{
		SchemaNamingFormat = EnsureValidFormat(format, "SchemaNamingFormat");
		return (TSelf)this;
	}

	public TSelf OverrideSchemaTypeNamingFormat(string format)
	{
		SchemaTypeNamingFormat = EnsureValidFormat(format, "SchemaTypeNamingFormat");
		return (TSelf)this;
	}

	public TSelf ResolveTypesFromAssemblyContaining<T>()
	{
		if (Assemblies.Contains(typeof(T).Assembly))
		{
			throw new InvalidOperationException($"Assembly {typeof(T).Assembly.FullName} is already added");
		}

		Assemblies.Add(typeof(T).Assembly);
		return (TSelf)this;
	}
	
	public TSelf SetDirectories(Action<TDirectoryOutputConfiguration> action)
	{
		var directoryOutputConfiguration = new TDirectoryOutputConfiguration();
		action(directoryOutputConfiguration);
		DirectoryOutputConfiguration = directoryOutputConfiguration;

		return (TSelf)this;
	}
	#endregion

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
		where TSchemaToUpsert : TAtomicSchema, new()
	{
		var type = typeof(TType);

		if (AtomDictionary.ContainsKey(type))
		{
			AtomDictionary[type] = substituteFactory is null
				? new TSchemaToUpsert()
				: substituteFactory();
		}
		else
		{
			AtomDictionary.Add(type, substituteFactory is null
				? new TSchemaToUpsert()
				: substituteFactory());
		}
	}

	private static string EnsureValidFormat(string format, string formatType)
	{
		var exceptions = new List<Exception>();
		if (!format.Contains("{0}"))
		{
			exceptions.Add(new ArgumentException($"The format <{formatType}> must contain a {{0}} placeholder"));
		}

		if (PlaceholderRegex().IsMatch(format))
		{
			exceptions.Add(new ArgumentException($"The format <{formatType}> must not contain any other placeholders than {0}"));
		}

		if (exceptions.Any())
		{
			throw new AggregateException(exceptions);
		}

		return format;
	}

	[GeneratedRegex("""\{[1-9]+\}""", RegexOptions.Compiled | RegexOptions.IgnoreCase)]
    private static partial Regex PlaceholderRegex();
}
