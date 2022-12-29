using Oxx.Backend.Generators.PocoSchema.Core.Models.Pocos.Contracts;

namespace Oxx.Backend.Generators.PocoSchema.Core.Models.Pocos;

public record class PocoEnum(Type EnumType) : IPocoStructure
{
	public Type EnumType { get; init; } = EnumType.IsEnum
		? EnumType
		: throw new ArgumentException("Type must be an enum", nameof(EnumType));

	public string TypeName => EnumType.Name;
}
