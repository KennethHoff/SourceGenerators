using Oxx.Backend.Generators.PocoSchema.Zod;
using TestingApp.Models;

namespace TestingApp;

internal static class SchemaGeneratorConfiguration
{
	public static ZodSchemaGeneratorConfigurationBuilder ConfigureSchemaGenerator()
	{
		var configurationBuilder = new ZodSchemaGeneratorConfigurationBuilder();
		configurationBuilder.ResolveTypesFromAssemblyContaining<RenamedYetAgain>();

		return configurationBuilder;
	}
}
