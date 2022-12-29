using System.Collections;
using System.Reflection;
using Namotion.Reflection;
using Oxx.Backend.Generators.PocoSchema.Zod.Configuration;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts.Models;

namespace Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.BuiltIn;

public class ArrayBuiltInAtomicZodSchema<TUnderlyingSchema> : IGenericZodSchema, IAdditionalImportZodSchema, IBuiltInAtomicZodSchema
	where TUnderlyingSchema : IPartialZodSchema, new()
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

	private ContextualType ListElement => PropertyInfo.ToContextualProperty().PropertyType.GenericArguments.First();

	public SchemaDefinition SchemaDefinition
	{
		get
		{
			var schemaName = Configuration.FormatSchemaName(UnderlyingSchema);

			if (ListElement.Nullability is Nullability.Nullable)
			{
				schemaName += ".nullable()";
			}

			return new SchemaDefinition($"z.array({schemaName})");
		}
	}
	
	private IPartialZodSchema UnderlyingSchema => Configuration.CreatedSchemasDictionary.GetSchemaForType(PropertyInfo.PropertyType.GetGenericArguments()[0])
	?? throw new InvalidOperationException($"Could not find schema for type {PropertyInfo.PropertyType.GetGenericArguments()[0]}");
}
