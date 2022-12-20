using Oxx.Backend.Generators.PocoSchema.Core;
using Oxx.Backend.Generators.PocoSchema.Zod;
using Oxx.Backend.Generators.PocoSchema.Zod.Configuration;
using TestingApp;

var configuration = new ZodSchemaGeneratorConfigurationBuilder()
	.SetRootDirectory("""C:\OXX\Projects\Suppehue\Suppehue.Frontend.NextJS\src\zod""")
	.ResolveTypesFromAssemblyContaining<ITestingAppMarker>()
	.Build();

var schema = new ZodSchema(configuration);
var generator = new SchemaGenerator(schema, configuration);
generator.CreateFiles();