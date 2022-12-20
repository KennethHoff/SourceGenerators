using Oxx.Backend.Generators.PocoSchema.Zod.Configuration;
using TestingApp.Models;

namespace TestingApp;

internal static class SchemaGeneratorConfiguration
{
	public static ZodSchemaGeneratorConfigurationBuilder ConfigureSchemaGenerator()
	{
		var configurationBuilder = new ZodSchemaGeneratorConfigurationBuilder();
		configurationBuilder.ResolveTypesFromAssemblyContaining<NoobaTron>();

		return configurationBuilder;
	}
}
