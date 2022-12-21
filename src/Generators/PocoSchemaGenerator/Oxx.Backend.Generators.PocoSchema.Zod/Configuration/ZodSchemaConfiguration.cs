using System.Reflection;
using Oxx.Backend.Generators.PocoSchema.Core.Configuration;
using Oxx.Backend.Generators.PocoSchema.Zod.Configuration.Events;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;

namespace Oxx.Backend.Generators.PocoSchema.Zod.Configuration;

public class ZodSchemaConfiguration : ISchemaConfiguration<IZodSchemaType, ZodSchemaEventConfiguration>
{
	public required IEnumerable<Assembly> Assemblies { get; init; }
	public required string OutputDirectory { get; init; }
	public required bool DeleteFilesOnStart { get; init; } = true;
	public required IDictionary<Type, IZodSchemaType> SchemaTypeDictionary { get; init; }
	public required string PropertyNamingConvention { get; init; } = "{0}Schema";
	public required string PropertyTypeNamingConvention { get; init; } = "{0}SchemaType";
	public required ZodSchemaEventConfiguration Events { get; init; }
}