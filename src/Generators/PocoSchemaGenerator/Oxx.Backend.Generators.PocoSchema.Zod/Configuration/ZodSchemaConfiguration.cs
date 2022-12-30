using System.Reflection;
using Oxx.Backend.Generators.PocoSchema.Core.Configuration;
using Oxx.Backend.Generators.PocoSchema.Core.Configuration.Abstractions;
using Oxx.Backend.Generators.PocoSchema.Core.Models.Types;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.BuiltIn;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.BuiltIn.Contracts;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts.Models;

namespace Oxx.Backend.Generators.PocoSchema.Zod.Configuration;

public class ZodSchemaConfiguration : ISchemaConfiguration<ZodSchemaEvents>
{
	public required IEnumerable<Assembly> Assemblies { get; init; }
	public required FileDeletionMode FileDeletionMode { get; init; }
	public required ZodSchemaEvents Events { get; init; }
	public required string FileExtension { get; init; }
	public required string FileExtensionInfix { get; init; }

	public required DirectoryInfo OutputDirectory { get; init; }
	public required string SchemaFileNameFormat { get; init; }
	public required string SchemaNamingFormat { get; init; }
	public required string SchemaTypeNamingFormat { get; init; }

	/// <summary>
	///     Dictionary containing the non-generic types that will be generated <br />
	///     Don't use this if you want to find the schema to use for other types. <br />
	///     Use <see cref="CreatedSchemasDictionary" /> instead.
	/// </summary>
	public required TypeSchemaDictionary<IPartialZodSchema> AtomicSchemasToCreateDictionary { get; init; }

	/// <summary>
	///     Dictionary containing fully created schemas
	/// </summary>
	public required TypeSchemaDictionary<IPartialZodSchema> CreatedSchemasDictionary { get; set; }

	/// <summary>
	///     Dictionary containing the generic types that will be generated
	/// </summary>
	public required TypeTypeDictionary GenericSchemasDictionary { get; init; }

	public required string SchemaEnumNamingFormat { get; init; }

	public IPartialZodSchema CreateGenericSchema(SchemaMemberInfo memberInfo)
	{
		var genericTypeDefinition = memberInfo.Type.GetGenericTypeDefinition();
		var genericArguments = memberInfo.Type.GetGenericArguments();
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


		Type genericSchemaType;
		try
		{
			genericSchemaType = genericSchema.MakeGenericType(typeArguments);
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}

		// if genericSchemaType does not implement IGenericZodSchema, throw an exception
		if (!typeof(IGenericZodSchema).IsAssignableFrom(genericSchemaType))
		{
			throw new InvalidOperationException($"The generic schema type {genericSchemaType} does not implement {nameof(IGenericZodSchema)}.");
		}

		var partialZodSchema = (IGenericZodSchema)Activator.CreateInstance(genericSchemaType)!;
		partialZodSchema.Configuration = this;
		partialZodSchema.MemberInfo = memberInfo;
		return partialZodSchema;
	}

	public ZodImport CreateStandardImport(IPartialZodSchema schema)
		=> schema switch
		{
			IBuiltInAtomicZodSchema => ZodImport.None,
			_                       => new ZodImport(FormatSchemaName(schema), FormatFilePath(schema)),
		};

	public string FormatEnumName(IEnumZodSchema schema)
		=> string.Format(SchemaEnumNamingFormat, schema.SchemaBaseName);

	public string FormatFilePath(IPartialZodSchema schema)
		=> $"./{FormatSchemaName(schema)}{FileExtensionInfix}";

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

	public IPartialZodSchema CreateArraySchema(SchemaMemberInfo schemaMemberInfo)
	{
		var elementType = schemaMemberInfo.Type.GetElementType()!;
		var elementSchema = CreatedSchemasDictionary.GetSchemaForType(elementType);
		if (elementSchema is null)
		{
			throw new InvalidOperationException($"Could not find schema for array element type {elementType.Name}.");
		}
		
		var arraySchemaType = typeof(ArrayBuiltInAtomicZodSchema<>).MakeGenericType(elementSchema.GetType());
		var arraySchema = (IGenericZodSchema)Activator.CreateInstance(arraySchemaType)!;

		arraySchema.Configuration = this;
		arraySchema.MemberInfo = schemaMemberInfo;
		return arraySchema;
	}
}
