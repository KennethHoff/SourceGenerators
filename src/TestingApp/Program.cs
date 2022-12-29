using AnotherProject;
using Oxx.Backend.Generators.PocoSchema.Zod;
using Oxx.Backend.Generators.PocoSchema.Zod.Configuration;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.BuiltIn;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Custom;
using TestingApp;
using TestingApp.Models;
using TestingApp.Models.Seremonibasen.Models;
using TestingApp.SchemaTypes;

var configuration = new ZodSchemaConfigurationBuilder()
	.SetRootDirectory("""C:\OXX\Projects\Suppehue\Suppehue.Frontend.NextJS\src\zod""")
	.DeleteExistingFiles()
	.ResolveTypesFromAssemblyContaining<ITestingAppAssemblyMarker>()
	.ApplyAtomicSchema<Localization, StringBuiltInAtomicZodSchema>()
	// .ResolveTypesFromAssemblyContaining<IAnotherProjectAssemblyMarker>()
	// .ApplyAtomicSchema<PersonId, TypedIdAtomicZodSchema<PersonId>>()
	// .ApplyAtomicSchema<PersonId?, StringBuiltInAtomicZodSchema>()
	// .ApplyAtomicSchema<CeremonyId, TypedIdAtomicZodSchema<CeremonyId>>()
	// .ApplyAtomicSchema<ClampedNumber, ClampedNumberAtomicZodSchema>(() => new ClampedNumberAtomicZodSchema(..10))
	.ConfigureEvents(events =>
	{
		events.MoleculeSchemaCreated += (_, args) =>
		{
			if (args.InvalidProperties.Count is 0)
			{
				return;
			}

			var errorMessage = $"Unable to resolve schema for the following properties on <{args.Type.FullName}>: " + Environment.NewLine +
							   string.Join(Environment.NewLine, args.InvalidProperties.Select(p => $"{p.Name} ({p.PropertyType})"));
			Console.WriteLine(errorMessage + Environment.NewLine);
		};
	})
	.Build();

var schema = new ZodSchemaConverter(configuration);
var generator = new ZodSchemaGenerator(schema, configuration);

await generator.CreateFilesAsync();