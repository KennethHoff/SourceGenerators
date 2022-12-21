using Oxx.Backend.Generators.PocoSchema.Core.Models;
using Oxx.Backend.Generators.PocoSchema.Zod;
using Oxx.Backend.Generators.PocoSchema.Zod.Configuration;
using TestingApp;

var configuration = new ZodSchemaConfigurationBuilder()
	.SetRootDirectory("/home/kennethhoff/Documents/Development/OXX/Suppehue/Frontend/Suppehue.Frontend.NextJS/src/zod/")
	.ResolveTypesFromAssemblyContaining<ITestingAppMarker>()
	.ConfigureEvents(events =>
	{
		events.FilesCreating += (sender, args) =>
		{
			args.FileInformations.Add(new FileInformation
			{
				Name = "test.ts",
				Content = "test",
			});
		};
		events.FileCreating += (_, args) =>
		{
			if (args.SchemaInformation.Name.Contains("Noob"))
			{
				args.Skip = true;
			}
			else
			{
				Console.WriteLine($"Creating file {args.SchemaInformation.Name}");
			}
		};
	})
	.Build();

var schema = new ZodSchema(configuration);
var generator = new ZodSchemaGenerator(schema, configuration);
generator.CreateFiles();