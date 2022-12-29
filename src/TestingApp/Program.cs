using AnotherProject;
using AnotherProject.Seremonibasen.Models;
using Oxx.Backend.Generators.PocoSchema.Core.Configuration.Events.Models;
using Oxx.Backend.Generators.PocoSchema.Zod;
using Oxx.Backend.Generators.PocoSchema.Zod.Configuration;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.BuiltIn;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Custom;
using TestingApp;
using TestingApp.Models;

var configuration = new ZodSchemaConfigurationBuilder()
	.SetRootDirectory("""C:\OXX\Projects\Suppehue\Suppehue.Frontend.NextJS\src\zod""")
	.DeleteExistingFiles()
	.ResolveTypesFromAssemblyContaining<ITestingAppAssemblyMarker>()
	.ResolveTypesFromAssemblyContaining<IAnotherProjectAssemblyMarker>()
	.ApplyAtomicSchema<Localization, StringBuiltInAtomicZodSchema>()
	// .ResolveTypesFromAssemblyContaining<IAnotherProjectAssemblyMarker>()
	.ApplyAtomicSchema<PersonId, TypedIdAtomicZodSchema<PersonId>>()
	// .ApplyAtomicSchema<CeremonyId, TypedIdAtomicZodSchema<CeremonyId>>()
	// .ApplyAtomicSchema<ClampedNumber, ClampedNumberAtomicZodSchema>(() => new ClampedNumberAtomicZodSchema(..10))
	.ConfigureEvents(events =>
	{
		// events.MoleculeSchemaCreated += (_, args) =>
		// {
		// 	if (args.Information.InvalidMembers.Count is 0)
		// 	{
		// 		return;
		// 	}
		//
		// 	PrintInvalidProperties(args.Information);
		// };

		events.MoleculeSchemasCreated += (_, args) =>
		{
			PrintTotalInvalidProperties(args.Informations);
		};
	})
	.Build();

var schema = new ZodSchemaConverter(configuration);
var generator = new ZodSchemaGenerator(schema, configuration);

await generator.CreateFilesAsync();

// void PrintInvalidProperties(CreatedSchemaInformation args)
// {
// 	Console.Write("Unable to resolve schema for the following ");
// 	Console.ForegroundColor = ConsoleColor.Red;
// 	Console.Write(args.InvalidMembers.Count);
// 	Console.ResetColor();
// 	Console.Write(" members of ");
// 	Console.ForegroundColor = ConsoleColor.Cyan;
// 	Console.Write(args.Type.FullName);
// 	Console.ResetColor();
// 	Console.WriteLine(":");
// 	foreach (var member in args.InvalidMembers)
// 	{
// 		Console.ForegroundColor = ConsoleColor.Yellow;
// 		Console.Write(member.MemberName);
// 		Console.ResetColor();
// 		Console.Write(" (");
// 		Console.ForegroundColor = ConsoleColor.Magenta;
// 		Console.Write(member.MemberType);
// 		Console.ResetColor();
// 		Console.WriteLine(")");
// 	}
// }

void PrintTotalInvalidProperties(IReadOnlyCollection<CreatedSchemaInformation> informations)
{
	var informationsWithInvalidMembers = informations
		.Where(x => x.InvalidMembers.Any())
		.ToList();

	if (informationsWithInvalidMembers.Count is 0)
	{
		Console.Write("All ");
		Console.ForegroundColor = ConsoleColor.Green;
		Console.Write(informations.Count);
		Console.ResetColor();
		Console.WriteLine(" schemas were created successfully.");
		return;
	}

	var typesWithoutSchemas = informations
		.SelectMany(information => information.InvalidMembers)
		.Select(memberInfo => memberInfo.MemberType)
		.Distinct()
		.ToArray();

	var invalidSchemasAmount = typesWithoutSchemas.Length;
	var invalidTypesAmount = informationsWithInvalidMembers.Count;

	PrintTotal();
	PrintInvalid();
	PrintTypesWithoutSchemas();

	void PrintTotal()
	{
		Console.ForegroundColor = ConsoleColor.Cyan;
		Console.Write(informations.Count);
		Console.ResetColor();
		Console.Write(" types were resolved, of which ");
		Console.ForegroundColor = ConsoleColor.Green;
		Console.Write(informations.Count - invalidTypesAmount);
		Console.ResetColor();
		Console.WriteLine(" were resolved fully.");
	}
	
	void PrintInvalid()
	{
		Console.ForegroundColor = ConsoleColor.Red;
		Console.Write(invalidSchemasAmount);
		Console.ResetColor();
		Console.Write(" schemas could not be resolved in ");
		Console.ForegroundColor = ConsoleColor.Cyan;
		Console.Write(invalidTypesAmount);
		Console.ResetColor();
		Console.WriteLine(" types.");
	}
	
	void PrintTypesWithoutSchemas()
	{
		Console.Write("The following ");
		Console.ForegroundColor = ConsoleColor.Red;
		Console.Write(invalidSchemasAmount);
		Console.ResetColor();
		Console.WriteLine(" schemas could not be resolved:");
		Console.ForegroundColor = ConsoleColor.Cyan;
		foreach (var type in typesWithoutSchemas)
		{
			Console.WriteLine(type);
		}
		Console.ResetColor();
		Console.WriteLine("Keep in mind that generics might have failed to resolve due to their type arguments being unable to resolve.");
	}
}