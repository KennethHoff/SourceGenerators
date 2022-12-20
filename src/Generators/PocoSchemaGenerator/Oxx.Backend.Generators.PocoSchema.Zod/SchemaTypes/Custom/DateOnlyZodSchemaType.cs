using Oxx.Backend.Generators.PocoSchema.Core;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;

namespace Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Custom;

// Check if this actually works.
public class DateOnlyZodSchemaType : IZodSchemaType
{
	public SchemaLogic ValidationSchemaLogic => new("""z.string().regex(/^[0-9]{4}-[0-9]{2}-[0-9]{2}$/).transform(x => new Date(x)).brand<"DateOnly">()""");
	public BaseName ValidationSchemaName => new("dateOnly");
}
