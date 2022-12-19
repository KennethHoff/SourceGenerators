using Oxx.Backend.Generators.PocoSchema.Zod;
using TestingApp.Models;

namespace TestingApp;

internal static class SchemaGeneratorConfiguration
{
	public static ZodSchemaGeneratorConfigurationBuilder ConfigureZodSchema()
	{
		var configurationBuilder = new ZodSchemaGeneratorConfigurationBuilder();
		configurationBuilder.ResolveTypesFromAssemblyContaining<TestPoco>();

		return configurationBuilder;
	}
}
