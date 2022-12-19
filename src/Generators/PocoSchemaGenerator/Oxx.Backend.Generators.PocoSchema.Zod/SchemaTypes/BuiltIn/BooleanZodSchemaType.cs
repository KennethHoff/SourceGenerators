using Oxx.Backend.Generators.PocoSchema.Core;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;

namespace Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.BuiltIn;

public class BooleanZodSchemaType : IBuiltInZodSchemaType
{
	public SchemaLogic ValidationSchemaLogic => new("z.boolean()");
	public BaseName ValidationSchemaName => new("boolean");
}