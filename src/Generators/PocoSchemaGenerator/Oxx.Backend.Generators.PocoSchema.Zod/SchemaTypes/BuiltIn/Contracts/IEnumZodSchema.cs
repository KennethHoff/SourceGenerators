using Oxx.Backend.Generators.PocoSchema.Core.Extensions;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;

namespace Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.BuiltIn.Contracts;

public interface IEnumZodSchema : IAtomicZodSchema
{
	string EnumContent { get; }
	Type EnumType { get; }
	string EnumValuesString { get; }
	IReadOnlyCollection<EnumValue> EnumValuesWithNames { get; }
}
