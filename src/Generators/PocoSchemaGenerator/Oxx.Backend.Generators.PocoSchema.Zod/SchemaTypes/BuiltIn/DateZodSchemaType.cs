using Oxx.Backend.Generators.PocoSchema.Zod.Core;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;

namespace Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.BuiltIn;

public class DateZodSchemaType : IBuiltInZodSchemaType
{
	public SchemaLogic ValidationSchemaLogic => new("z.date()");
	public BaseName ValidationSchemaName => new("date");
}