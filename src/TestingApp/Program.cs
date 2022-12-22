using AnotherProject;
using Oxx.Backend.Generators.PocoSchema.Zod;
using Oxx.Backend.Generators.PocoSchema.Zod.Configuration;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Custom;
using TestingApp;
using TestingApp.Models;
using TestingApp.SchemaTypes;

var configuration = new ZodSchemaConfigurationBuilder()
	.SetRootDirectory("/home/kennethhoff/Documents/Development/OXX/Suppehue/Frontend/Suppehue.Frontend.NextJS/src/zod/")
	.DeleteExistingFiles()
	.OverrideFileNameNamingFormat("{0}")
	.OverrideSchemaTypeNamingFormat("{0}")
	.ResolveTypesFromAssemblyContaining<ITestingAppAssemblyMarker>()
	.ResolveTypesFromAssemblyContaining<IAnotherProjectAssemblyMarker>()
	.SubstituteIncludingNullable<PersonId, TypedIdAtomicZodSchema<PersonId>>()
	.SubstituteIncludingNullable<CeremonyId, TypedIdAtomicZodSchema<CeremonyId>>()
	.SubstituteExcludingNullable<ClampedNumber, ClampedNumberAtomicZodSchema>(() => new ClampedNumberAtomicZodSchema(..10))
	.Build();

var schema = new ZodSchemaConverter(configuration);
var generator = new ZodSchemaGenerator(schema, configuration);

await generator.CreateFilesAsync();