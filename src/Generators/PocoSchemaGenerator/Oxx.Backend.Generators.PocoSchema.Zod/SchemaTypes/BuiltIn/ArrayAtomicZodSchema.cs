using Oxx.Backend.Generators.PocoSchema.Zod.Configuration;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts.Models;

namespace Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.BuiltIn;

public class ArrayAtomicZodSchema<TUnderlyingSchema> : IAtomicZodSchema, IGenericZodSchema
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
			return new SchemaDefinition($"z.array({Configuration.FormatSchemaName(partialZodSchema)})");
		}
	}

	public SchemaBaseName SchemaBaseName
	{
		get
		{
			IZodSchema partialZodSchema = new TUnderlyingSchema();
			return new SchemaBaseName($"Array{partialZodSchema.SchemaBaseName}");
		}
	}
}