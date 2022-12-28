using AnotherProject;
using Oxx.Backend.Generators.PocoSchema.Zod;
using Oxx.Backend.Generators.PocoSchema.Zod.Configuration;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.BuiltIn;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Custom;
using TestingApp;
using TestingApp.Models;
using TestingApp.SchemaTypes;

var configuration = new ZodSchemaConfigurationBuilder()
	.SetRootDirectory("""C:\OXX\Projects\Suppehue\Suppehue.Frontend.NextJS\src\zod""")
	.DeleteExistingFiles()
	.ResolveTypesFromAssemblyContaining<ITestingAppAssemblyMarker>()
	.ResolveTypesFromAssemblyContaining<IAnotherProjectAssemblyMarker>()
	.ApplySchema<PersonId, TypedIdAtomicZodSchema<PersonId>>()
	.ApplySchema<PersonId?, StringBuiltInAtomicZodSchema>()
	.ApplySchema<CeremonyId, TypedIdAtomicZodSchema<CeremonyId>>()
	.ApplySchema<ClampedNumber, ClampedNumberAtomicZodSchema>(() => new ClampedNumberAtomicZodSchema(..10))
	.Build();

var schema = new ZodSchemaConverter(configuration);
var generator = new ZodSchemaGenerator(schema, configuration);

await generator.CreateFilesAsync();