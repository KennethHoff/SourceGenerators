using Oxx.Backend.Generators.PocoSchema.Zod.Configuration;
using TestingApp.Models;

namespace TestingApp;

internal static class SchemaGeneratorConfiguration
{
	public static ZodSchemaConfigurationBuilder ConfigureSchemaGenerator()
	{
		var configurationBuilder = new ZodSchemaConfigurationBuilder();
		configurationBuilder.ResolveTypesFromAssemblyContaining<NoobaTron>();

		return configurationBuilder;
	}
}
