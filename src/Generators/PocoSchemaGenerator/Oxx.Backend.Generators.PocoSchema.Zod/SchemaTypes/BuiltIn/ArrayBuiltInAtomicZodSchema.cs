using System.Reflection;
using Namotion.Reflection;
using Oxx.Backend.Generators.PocoSchema.Core.Extensions;
using Oxx.Backend.Generators.PocoSchema.Zod.Configuration;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts.Models;

namespace Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.BuiltIn;

public class ArrayBuiltInAtomicZodSchema<TUnderlyingSchema> : IGenericZodSchema, IAdditionalImportZodSchema, IBuiltInAtomicZodSchema
	where TUnderlyingSchema: IZodSchema, new()
{
	public ZodSchemaConfiguration Configuration { get; private set; } = null!;
	public PropertyInfo PropertyInfo { get; private set; } = null!;

	public void SetConfiguration(ZodSchemaConfiguration configuration)
	{
		Configuration = configuration;
	}
	
	public void SetPropertyInfo(PropertyInfo propertyInfo)
	{
		PropertyInfo = propertyInfo;
	}

	public SchemaDefinition SchemaDefinition
	{
		get
		{
			IZodSchema partialZodSchema = new TUnderlyingSchema();
			var schemaName = Configuration.FormatSchemaName(partialZodSchema);

			var list = PropertyInfo.ToContextualProperty();
			var listElement = list.PropertyType.GenericArguments.First();

			if (listElement.Nullability is Nullability.Nullable)
			{
				schemaName += ".nullable()";
			}
			return new SchemaDefinition($"z.array({schemaName})");
		}
	}

	public IDictionary<PropertyInfo, IPartialZodSchema> SchemaDictionary => new Dictionary<PropertyInfo, IPartialZodSchema>
	{
		{ typeof(TUnderlyingSchema).GetProperty(nameof(IZodSchema.SchemaDefinition))!, new TUnderlyingSchema() },
	};

	public IEnumerable<ZodImport> AdditionalImports
	{
		get
		{
			if (SchemaDictionary.TryGetValue(typeof(TUnderlyingSchema).GetProperty(nameof(IZodSchema.SchemaDefinition))!, out var schema))
			{
				yield return Configuration.CreateStandardImport(schema);
			}
		}
	}
}