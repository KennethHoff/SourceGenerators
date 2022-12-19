using Oxx.Backend.Generators.PocoSchema.Core;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;

namespace Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.BuiltIn;

public class StringZodSchemaType : IBuiltInZodSchemaType
{
	public SchemaLogic ValidationSchemaLogic => new("z.string()");
	public BaseName ValidationSchemaName => new("string");
}