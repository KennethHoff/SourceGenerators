using Oxx.Backend.Generators.PocoSchema.Core;
using Oxx.Backend.Generators.PocoSchema.Zod;
using Oxx.Backend.Generators.PocoSchema.Zod.Configuration;
using TestingApp.Models;

var configuration = new ZodSchemaGeneratorConfigurationBuilder()
	.SetRootDirectory("/home/kennethhoff/Documents/Development/OXX/Suppehue/Frontend/Suppehue.Frontend.NextJS/src/zod/")
	.ResolveTypesFromAssemblyContaining<RenamedYetAgain>()
	.Build();

var schema = new ZodSchema(configuration);
var generator = new SchemaGenerator(schema, configuration);
generator.CreateFiles();