using Oxx.Backend.Generators.PocoSchema.Core.Extensions;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.BuiltIn.Contracts;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts.Models;

namespace Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.BuiltIn;

public record class EnumZodSchema(Type EnumType) : IEnumZodSchema
{
	public Type EnumType { get; init; } = EnumType.IsEnum
		? EnumType
		: throw new ArgumentException("Type must be an enum", nameof(EnumType));

	// Don't use this
	public SchemaDefinition SchemaDefinition => new(EnumType.Name);
	
	public IReadOnlyCollection<EnumValue> EnumValuesWithNames => TypeExtensions.GetEnumValues(EnumType);
	
	public string EnumContent => "[" + string.Join(", ", EnumValuesWithNames.Select(x => $"\"{x.Name}\"")) + "]";
	public string EnumValuesString => string.Join($",{Environment.NewLine}", EnumValuesWithNames.Select(x => $"\t{x.Name} = {x.Value}"));
	public SchemaBaseName SchemaBaseName => new(EnumType.Name);
}