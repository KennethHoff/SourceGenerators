using Oxx.Backend.Generators.PocoSchema.Core.Extensions;
using Oxx.Backend.Generators.PocoSchema.Core.Models.File;
using Oxx.Backend.Generators.PocoSchema.Core.Models.Poco;
using Oxx.Backend.Generators.PocoSchema.Zod.Configuration;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.BuiltIn.Contracts;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts.Models;

namespace Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.BuiltIn;

// TODO: Improve this, so that it generates a custom schema & type for each enum instead of a simple `z.enum` with all the values.
/*
 * enum Gender { Male, Female, Other }
 * ApplySchema<Gender, SimpleEnumBuiltInAtomicZodSchema<Gender>
 *
 * How it currently works:
 * -- person.ts --
 * personGender: z.enum("Male", "Female", "Other")
 * 
 * How I want it to work:
 * -- person.ts --
 * personGender: genderSchema
 *
 * -- genderSchema.ts --
 * export const genderSchema = z.enum("Male", "Female", "Other");
 * export type genderSchemaType = z.infer<typeof genderSchema>;
 */

// public class SimpleEnumBuiltInAtomicZodSchema<TEnum> : IBuiltInAtomicZodSchema
// 	where TEnum : struct, Enum
// {
// 	public SchemaDefinition SchemaDefinition => new($"z.enum([{string.Join(", ", Enum.GetValues<TEnum>().Select(x => $"\"{x}\""))}])");
// }

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