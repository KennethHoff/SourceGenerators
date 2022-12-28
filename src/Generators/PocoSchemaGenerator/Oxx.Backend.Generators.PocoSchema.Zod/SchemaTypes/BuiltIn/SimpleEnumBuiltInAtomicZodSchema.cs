using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;
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

public class SimpleEnumBuiltInAtomicZodSchema<TEnum> : IBuiltInAtomicZodSchema
	where TEnum : struct, Enum
{
	public SchemaDefinition SchemaDefinition 
		=> new($"z.enum([{string.Join(", ", Enum.GetValues<TEnum>().Select(x => $"\"{x}\""))}])");
}