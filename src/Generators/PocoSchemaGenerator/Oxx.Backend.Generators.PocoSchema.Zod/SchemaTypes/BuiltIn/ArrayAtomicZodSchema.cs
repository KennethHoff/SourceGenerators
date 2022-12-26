using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts.Models;

namespace Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.BuiltIn;

public class ArrayAtomicZodSchema<TUnderlyingSchema> : IBuiltInAtomicZodSchema
	where TUnderlyingSchema: IZodSchema, new()
{
	public SchemaDefinition SchemaDefinition
	{
		get
		{
			IPartialZodSchema partialZodSchema = new TUnderlyingSchema();
			var schemaTypeName = "guidSchema";
			return new SchemaDefinition($"z.array({schemaTypeName})");
		}
	}
}
