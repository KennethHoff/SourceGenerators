using AnotherProject;
using AnotherProject.Seremonibasen.Models;
using Oxx.Backend.Generators.PocoSchema.Core.Configuration.Events;
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
		events.PocoStructuresCreated += (_, args) => PrintPocoStructuresCreated(args);
		events.MoleculeSchemasCreated += (_, args) => PrintMoleculeSchemasCreated(args);
	})
	.Build();

var schema = new ZodSchemaConverter(configuration);
var generator = new ZodSchemaGenerator(schema, configuration);

await generator.CreateFilesAsync();





void PrintMoleculeSchemasCreated(MoleculeSchemasCreatedEventArgs eventArgs)
{
	var informations = eventArgs.Informations;

	var informationsWithInvalidMembers = informations
		.Where(x => x.InvalidMembers.Any())
		.ToList();

	switch (informationsWithInvalidMembers.Count)
	{
		case 0:
			PrintAllSchemasResolved();
			break;
		default:
			PrintTotal();
			PrintTypesWithoutSchemas();
			break;
	}
	
	Console.WriteLine();

	void PrintTotal()
	{
		var invalidTypesAmount = informationsWithInvalidMembers.Count;
		Console.ForegroundColor = ConsoleColor.Cyan;
		Console.Write(informations.Count);
		Console.ResetColor();
		Console.Write(" type-schemas were resolved, of which ");
		Console.ForegroundColor = ConsoleColor.Green;
		Console.Write(informations.Count - invalidTypesAmount);
		Console.ResetColor();
		Console.WriteLine(" were resolved fully.");
	}
	
	void PrintTypesWithoutSchemas()
	{
		var typesWithoutSchemas = informations
			.SelectMany(information => information.InvalidMembers)
			.Select(memberInfo => memberInfo.Type)
			.Distinct()
			.ToArray();

		var invalidSchemasAmount = typesWithoutSchemas.Length;
		Console.Write("The following ");
		Console.ForegroundColor = ConsoleColor.Red;
		Console.Write(invalidSchemasAmount);
		Console.ResetColor();
		Console.Write(" schemas could not be resolved in ");
		Console.ForegroundColor = ConsoleColor.Cyan;
		Console.Write(invalidSchemasAmount);
		Console.ResetColor();
		Console.WriteLine(" type-schemas:");
		Console.ForegroundColor = ConsoleColor.Cyan;
		foreach (var type in typesWithoutSchemas)
		{
			var typesContainingSchema = informations
				.Where(information => information.InvalidMembers.Any(memberInfo => memberInfo.Type == type))
				.Select(information => information.Type)
				.ToArray();
			
			var firstTwoTypes = typesContainingSchema.Take(2).ToArray();
			var length = typesContainingSchema.Length;
			Console.WriteLine(type + " (" + length + "): " + string.Join(", ", firstTwoTypes.Select(x => x.Name)) + (length > 2 ? ", ..." : ""));
		}
		Console.ResetColor();
		Console.WriteLine("Keep in mind that generics might have failed to resolve due to their type arguments being unable to be resolved.");
	}

	void PrintAllSchemasResolved()
	{
		Console.Write("All ");
		Console.ForegroundColor = ConsoleColor.Green;
		Console.Write(informations.Count);
		Console.ResetColor();
		Console.WriteLine(" type-schemas were created successfully.");
	}
}

void PrintPocoStructuresCreated(PocoStructuresCreatedEventArgs eventArgs)
{
	var unsupportedTypes = eventArgs.UnsupportedTypes;
	
	switch (unsupportedTypes.Count)
	{
		case 0:
			PrintAllTypesResolved();
			break;
		default:
			PrintSuccessfulStructures();
			PrintUnsupportedTypes();
			break;
	}
	Console.WriteLine();

	
	void PrintUnsupportedTypes()
	{
		Console.Write("The following ");
		Console.ForegroundColor = ConsoleColor.Red;
		Console.Write(unsupportedTypes.Count);
		Console.ResetColor();
		Console.WriteLine(" types could not be resolved:");
		Console.ForegroundColor = ConsoleColor.Cyan;
		foreach (var (type, exception) in unsupportedTypes)
		{
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write(type);
			Console.ResetColor();
			Console.Write(" (");
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.Write(exception.Message);
			Console.ResetColor();
			Console.WriteLine(")");
		}

		Console.ResetColor();
		Console.WriteLine();
	}

	void PrintSuccessfulStructures()
	{
		Console.Write("Successfully resolved ");
		Console.ForegroundColor = ConsoleColor.Green;
		Console.Write(eventArgs.PocoStructures.Length);
		Console.ResetColor();
		Console.Write(" out of ");
		Console.ForegroundColor = ConsoleColor.Cyan;
		Console.Write(eventArgs.PocoStructures.Length + unsupportedTypes.Count);
		Console.ResetColor();
		Console.WriteLine(" types.");
	}

	void PrintAllTypesResolved()
	{
		Console.Write("All ");
		Console.ForegroundColor = ConsoleColor.Cyan;
		Console.Write(eventArgs.PocoStructures.Length);
		Console.ResetColor();
		Console.WriteLine(" types were resolved successfully.");
	}
}
