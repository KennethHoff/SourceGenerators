using System.Reflection;
using Oxx.Backend.Generators.PocoSchema.Core.Configuration;
using Oxx.Backend.Generators.PocoSchema.Core.Models;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;

namespace Oxx.Backend.Generators.PocoSchema.Zod.Configuration;

public class ZodSchemaConfiguration : ISchemaConfiguration<IPartialZodSchema, ZodSchemaEventConfiguration>
{
	public required IEnumerable<Assembly> Assemblies { get; init; }
	public required bool DeleteFilesOnStart { get; init; }
	public required ZodSchemaEventConfiguration Events { get; init; }
	public required string FileExtension { get; init; }
	public required string OutputDirectory { get; init; }
	public required string SchemaFileNameFormat { get; init; }
	public required string SchemaNamingFormat { get; init; }
	public required string SchemaTypeNamingFormat { get; init; }
	
	/// <summary>
	/// Dictionary containing the generic types that will be generated
	/// </summary>
	public required TypeTypeDictionary GenericSchemasDictionary { get; init; }
	
	/// <summary>
	/// Dictionary containing the non-generic types that will be generated <br />
	/// Don't use this if you want to find the schema to use for other types. <br />
	/// Use <see cref="CreatedSchemasDictionary"/> instead.
	/// </summary>
	public required TypeSchemaDictionary<IPartialZodSchema> AtomicSchemasToCreateDictionary { get; init; }
	
	/// <summary>
	/// Dictionary containing fully created schemas
	/// </summary>
	public required TypeSchemaDictionary<IPartialZodSchema> CreatedSchemasDictionary { get; set; }

	public IPartialZodSchema CreateGenericSchema(PropertyInfo propertyInfo)
	{
		var genericTypeDefinition = propertyInfo.PropertyType.GetGenericTypeDefinition();
		var genericArguments = propertyInfo.PropertyType.GetGenericArguments();
		var genericSchema = GenericSchemasDictionary.GetRelatedType(genericTypeDefinition);

		var argumentSchemas = genericArguments
			.Select(type =>
			{
				var schema = CreatedSchemasDictionary.GetSchemaForType(type);
				return (Schema: schema, Type: type);
			})
			.ToArray();

		if (argumentSchemas.Any(x => x.Schema is null))
		{
			var nullSchemas = string.Join(", ", argumentSchemas.Where(x => x.Schema is null).Select(x => x.Type.Name));
			throw new InvalidOperationException("Could not find schema for generic type arguments: " + nullSchemas);
		}

		var typeArguments = argumentSchemas.Select(x => x.Schema!.GetType()).ToArray();

		var genericSchemaType = genericSchema.MakeGenericType(typeArguments);

		// if genericSchemaType does not implement IGenericZodSchema, throw an exception
		if (!typeof(IGenericZodSchema).IsAssignableFrom(genericSchemaType))
		{
			throw new InvalidOperationException($"The generic schema type {genericSchemaType} does not implement {nameof(IGenericZodSchema)}.");
		}

		var partialZodSchema = (IGenericZodSchema)Activator.CreateInstance(genericSchemaType)!;
		partialZodSchema.Configuration = this;
		partialZodSchema.PropertyInfo = propertyInfo;
		return partialZodSchema;
	}

	public ZodImport CreateStandardImport(IPartialZodSchema schema)
		=> schema switch
		{
			IBuiltInAtomicZodSchema => ZodImport.None,
			_                       => new ZodImport(FormatSchemaName(schema), FormatFilePath(schema)),
		};

	public string FormatFilePath(IPartialZodSchema schema)
		=> $"./{FormatSchemaName(schema)}";

	public string FormatSchemaName(IPartialZodSchema schema)
		=> schema switch
		{
			IBuiltInAtomicZodSchema => schema.SchemaBaseName,
			_                       => string.Format(SchemaNamingFormat, schema.SchemaBaseName),
		};

	public string FormatSchemaTypeName(IPartialZodSchema schema)
		=> schema switch
		{
			IBuiltInAtomicZodSchema => schema.SchemaBaseName,
			_                       => string.Format(SchemaTypeNamingFormat, schema.SchemaBaseName),
		};
}
