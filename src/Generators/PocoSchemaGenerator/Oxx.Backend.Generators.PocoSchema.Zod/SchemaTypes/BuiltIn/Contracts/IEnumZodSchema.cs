using Oxx.Backend.Generators.PocoSchema.Core.Extensions;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;

namespace Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.BuiltIn.Contracts;

public interface IEnumZodSchema : IAtomicZodSchema
{
	public Type EnumType { get; }
	public IReadOnlyCollection<EnumValue> EnumValuesWithNames { get; }
	public string EnumContent { get; }
	string EnumValuesString { get; }
}
