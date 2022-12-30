using Oxx.Backend.Generators.PocoSchema.Core.Models.Types;
using Oxx.Backend.Generators.PocoSchema.Zod.Configuration;

namespace Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;

public interface IGenericZodSchema : IZodSchema
{
	ZodSchemaConfiguration Configuration { get; set; }
	SchemaMemberInfo MemberInfo { get; set; }
}