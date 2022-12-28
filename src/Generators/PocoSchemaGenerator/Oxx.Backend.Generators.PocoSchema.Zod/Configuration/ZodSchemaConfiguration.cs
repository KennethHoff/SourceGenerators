using System.Reflection;
using Oxx.Backend.Generators.PocoSchema.Core.Configuration;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;

namespace Oxx.Backend.Generators.PocoSchema.Zod.Configuration;

public class ZodSchemaConfiguration : ISchemaConfiguration<IPartialZodSchema, ZodSchemaEventConfiguration>
{
	public required IEnumerable<Assembly> Assemblies { get; init; }
	public required string OutputDirectory { get; init; }
	public required bool DeleteFilesOnStart { get; init; }
	public required IDictionary<Type, IPartialZodSchema> SchemaDictionary { get; init; }
	public required IDictionary<Type, Type> GenericSchemaDictionary { get; init; }
	public required string SchemaNamingFormat { get; init; }
	public required string SchemaTypeNamingFormat { get; init; }
	public required string SchemaFileNameFormat { get; init; }
	public required ZodSchemaEventConfiguration Events { get; init; }
	public required string FileExtension { get; init; }

	public string FormatSchemaTypeName(IPartialZodSchema schema)
		=> schema is IBuiltInAtomicZodSchema or IAdditionalImportZodSchema
			? schema.SchemaBaseName
			: string.Format(SchemaTypeNamingFormat, schema.SchemaBaseName);

	public string FormatSchemaName(IPartialZodSchema schema)
		=> schema is IBuiltInAtomicZodSchema or IAdditionalImportZodSchema
			? schema.SchemaBaseName
			: string.Format(SchemaNamingFormat, schema.SchemaBaseName);

	public string FormatFilePath(IPartialZodSchema schema)
		=> $"./{FormatSchemaName(schema)}";

	public IPartialZodSchema CreateGenericSchema(PropertyInfo propertyInfo)
	{
		var genericTypeDefinition = propertyInfo.PropertyType.GetGenericTypeDefinition();
		var genericArguments = propertyInfo.PropertyType.GetGenericArguments();
		var genericSchema = GenericSchemaDictionary[genericTypeDefinition];
		
		var argumentSchemas = genericArguments
			.Select(x => SchemaDictionary[x])
			.ToArray();
		var genericSchemaType = genericSchema.MakeGenericType(argumentSchemas.Select(x => x.GetType()).ToArray());
		
		// if genericSchemaType does not implement IGenericZodSchema, throw an exception
		if (!typeof(IGenericZodSchema).IsAssignableFrom(genericSchemaType))
		{
			throw new InvalidOperationException($"The generic schema type {genericSchemaType} does not implement {nameof(IGenericZodSchema)}.");
		}
		var partialZodSchema = (IGenericZodSchema)Activator.CreateInstance(genericSchemaType)!;
		partialZodSchema.SetConfiguration(this);
		
		var underlyingPropertyInfos = genericSchemaType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

		partialZodSchema.SetUnderlyingTypes(underlyingPropertyInfos);
		return partialZodSchema;
	}

	public IPartialZodSchema CreateSchema(Type type)
		=> SchemaDictionary[type];

	public ZodImport CreateStandardImport(IPartialZodSchema schema)
		=> schema switch
		{
			IBuiltInAtomicZodSchema or IAdditionalImportZodSchema => ZodImport.None,
			_                                                     => new ZodImport(FormatSchemaName(schema), FormatFilePath(schema)),
		};

	public TValue CreateGenericSchema<TValue>(PropertyInfo type)
	{
		throw new NotImplementedException();
	}
}