using System.Reflection;
using Oxx.Backend.Generators.PocoSchema.Zod.Configuration;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts.Models;

namespace Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.BuiltIn;

public class ArrayAtomicZodSchema<TUnderlyingSchema> : IGenericZodSchema, IBuiltInMolecularZodSchema
	where TUnderlyingSchema: IZodSchema, new()
{
	public ZodSchemaConfiguration Configuration { get; private set; } = null!;

	public void SetConfiguration(ZodSchemaConfiguration configuration)
	{
		Configuration = configuration;
	}

	public SchemaDefinition SchemaDefinition
	{
		get
		{
			IZodSchema partialZodSchema = new TUnderlyingSchema();
			var schemaName = Configuration.FormatSchemaName(partialZodSchema);
			return new SchemaDefinition($"z.array({schemaName})");
		}
	}

	public IDictionary<PropertyInfo, IPartialZodSchema> SchemaDictionary => new Dictionary<PropertyInfo, IPartialZodSchema>
	{
		{ typeof(TUnderlyingSchema).GetProperty(nameof(IZodSchema.SchemaDefinition))!, new TUnderlyingSchema() },
	};

	public IEnumerable<ZodImport> AdditionalImports => new[]
	{
		Configuration.CreateStandardImport(new TUnderlyingSchema()),
	};
}