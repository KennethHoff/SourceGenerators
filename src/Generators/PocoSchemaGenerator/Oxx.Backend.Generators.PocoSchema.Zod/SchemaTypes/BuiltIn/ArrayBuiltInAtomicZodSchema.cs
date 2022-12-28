using System.Reflection;
using Namotion.Reflection;
using Oxx.Backend.Generators.PocoSchema.Zod.Configuration;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts.Models;

namespace Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.BuiltIn;

public class ArrayBuiltInAtomicZodSchema<TUnderlyingSchema> : IGenericZodSchema, IAdditionalImportZodSchema, IBuiltInAtomicZodSchema
	where TUnderlyingSchema : IZodSchema, new()
{
	private ZodSchemaConfiguration? _configuration;
	private PropertyInfo? _propertyInfo;

	public IEnumerable<ZodImport> AdditionalImports => new[]
	{
		Configuration.CreateStandardImport(UnderlyingSchema),
	};

	public ZodSchemaConfiguration Configuration
	{
		get => _configuration ?? throw new InvalidOperationException("Configuration is null");
		set => _configuration = value;
	}

	public PropertyInfo PropertyInfo
	{
		get => _propertyInfo ?? throw new InvalidOperationException("PropertyInfo is null");
		set => _propertyInfo = value;
	}

	public SchemaDefinition SchemaDefinition
	{
		get
		{
			var schemaName = Configuration.FormatSchemaName(UnderlyingSchema);

			var list = PropertyInfo.ToContextualProperty();
			var listElement = list.PropertyType.GenericArguments.First();

			if (listElement.Nullability is Nullability.Nullable)
			{
				schemaName += ".nullable()";
			}

			return new SchemaDefinition($"z.array({schemaName})");
		}
	}
	
	private IPartialZodSchema UnderlyingSchema => Configuration.CreatedSchemaDictionary.GetSchemaForType(PropertyInfo.PropertyType.GetGenericArguments()[0])
	?? throw new InvalidOperationException($"Could not find schema for type {PropertyInfo.PropertyType.GetGenericArguments()[0]}");
}
