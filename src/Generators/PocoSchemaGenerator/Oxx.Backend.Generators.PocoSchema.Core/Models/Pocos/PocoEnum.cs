using Oxx.Backend.Generators.PocoSchema.Core.Models.Pocos.Contracts;

namespace Oxx.Backend.Generators.PocoSchema.Core.Models.Pocos;

public record class PocoEnum(Type Type) : IPocoStructure
{
	public Type Type { get; init; } = Type.IsEnum
		? Type
		: throw new ArgumentException("Type must be an enum", nameof(Type));

	public string Name => Type.Name;
}
