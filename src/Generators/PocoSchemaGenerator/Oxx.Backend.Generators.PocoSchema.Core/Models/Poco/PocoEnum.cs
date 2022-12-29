using Oxx.Backend.Generators.PocoSchema.Core.Models.Poco.Contracts;

namespace Oxx.Backend.Generators.PocoSchema.Core.Models.Poco;

public record class PocoEnum(System.Type EnumType) : IPocoStructure
{
	public string TypeName => EnumType.Name;

	public System.Type EnumType { get; init; } = EnumType.IsEnum
		? EnumType
		: throw new ArgumentException("Type must be an enum", nameof(EnumType));
}