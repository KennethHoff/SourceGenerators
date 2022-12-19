using System.Reflection;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Oxx.Backend.Generators.PocoSchema.Zod.Core;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.BuiltIn;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;

namespace Oxx.Backend.Generators.PocoSchema.Zod.Generator;

[Generator]
public sealed class ZodSchemaSourceGenerator : SchemaSourceGenerator<IZodSchemaType>
{
	protected override SchemaGeneratorConfigurationBuilder<IZodSchemaType> ConfigurationBuilder => null!;
	protected override ISchema Schema => null!;

	// private SchemaGeneratorConfigurationBuilder<IZodSchemaType> CreateConfigurationBuilderThroughReflection()
	// {
	// 	var configurationBuilder = LoadConfigurationBuilder();
	// 	AddDefaultSubstitutions(configurationBuilder);
	// 	
	// 	return configurationBuilder;
	// }
	//
	// private static void AddDefaultSubstitutions(ZodSchemaGeneratorConfigurationBuilder configurationBuilder)
	// {
	// 	configurationBuilder.Substitute<int, NumberZodSchemaType>();
	// 	configurationBuilder.Substitute<float, NumberZodSchemaType>();
	// 	configurationBuilder.Substitute<double, NumberZodSchemaType>();
	// 	configurationBuilder.Substitute<decimal, NumberZodSchemaType>();
	// 	configurationBuilder.Substitute<string, StringZodSchemaType>();
	// }
	//
	// private ZodSchemaGeneratorConfigurationBuilder LoadConfigurationBuilder()
	// {
	// 	var configurationMethod = GetConfigurationMethod();
	//
	// 	var result = configurationMethod.Invoke(null, null);
	// 	if (result is not ZodSchemaGeneratorConfigurationBuilder builder)
	// 	{
	// 		throw new InvalidOperationException("Configuration method must return a ZodSchemaGeneratorConfigurationBuilder.");
	// 	}
	//
	// 	return builder;
	// }
	//
	// private static MethodInfo GetConfigurationMethod()
	// {
	// 	// All assemblies available to the generator are loaded into the current AppDomain.
	// 	var allAssemblies = AppDomain.CurrentDomain.GetAssemblies();
	// 	// Load the assembly containing the configuration method.
	// 	
	// 	throw new Exception(allAssemblies.Aggregate(string.Empty, (s, a) => s + a.FullName + Environment.NewLine));
	// 	
	// 	// Has to be a file called `SchemaGeneratorConfiguration.cs` in the root of the project.
	// 	// var type = assembly.GetType("SchemaGeneratorConfiguration");
	// 	// if (type is null)
	// 	// {
	// 	// 	throw new InvalidOperationException("Could not find type `SchemaGeneratorConfiguration` in assembly.");
	// 	// }
	// 	//
	// 	// // Has to be a static method called `ConfigureZodSchema`
	// 	// var method = type.GetMethod("ConfigureZodSchema");
	// 	// if (method is null)
	// 	// {
	// 	// 	throw new InvalidOperationException("Could not find method `ConfigureZodSchema` in type `SchemaGeneratorConfiguration`.");
	// 	// }
	// 	//
	// 	// if (method.GetParameters().Any())
	// 	// {
	// 	// 	throw new InvalidOperationException("Configuration method must not have any parameters.");
	// 	// }
	// 	//
	// 	// if (method.ReturnType != typeof(ZodSchemaGeneratorConfigurationBuilder))
	// 	// {
	// 	// 	throw new InvalidOperationException("Configuration method must return a ZodSchemaGeneratorConfigurationBuilder.");
	// 	// }
	// 	//
	// 	// return method;
	// }
}