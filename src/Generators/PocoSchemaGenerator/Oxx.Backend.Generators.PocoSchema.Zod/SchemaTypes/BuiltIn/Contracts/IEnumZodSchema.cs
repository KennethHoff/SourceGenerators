using Oxx.Backend.Generators.PocoSchema.Core.Extensions;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;

namespace Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.BuiltIn.Contracts;

public interface IEnumZodSchema : IAtomicZodSchema
{
	Type EnumType { get; }
	IReadOnlyCollection<EnumValue> EnumValuesWithNames { get; }
	string EnumContent { get; }
	string EnumValuesString { get; }
}
