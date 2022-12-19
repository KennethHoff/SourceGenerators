using Oxx.Backend.Generators.PocoSchema.Core;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;

namespace Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.BuiltIn;

public class NumberZodSchemaType : IBuiltInZodSchemaType
{
	public SchemaLogic ValidationSchemaLogic => new("z.number()");
	public BaseName ValidationSchemaName => new("number");
}