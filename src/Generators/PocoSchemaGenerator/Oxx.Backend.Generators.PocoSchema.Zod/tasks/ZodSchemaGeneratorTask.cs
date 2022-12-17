using System.Reflection;
using Oxx.Backend.Generators.PocoSchema.Zod.Core;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes;

namespace Oxx.Backend.Generators.PocoSchema.Zod.tasks;

public sealed class ZodSchemaGeneratorTask : SchemaGeneratorTask<IZodSchemaType>
{
	protected override SchemaGeneratorConfigurationBuilder<IZodSchemaType> ConfigurationBuilder => CreateConfigurationBuilderThroughReflection();
	protected override ISchema Schema => new ZodSchema(ConfigurationBuilder);
	
	private SchemaGeneratorConfigurationBuilder<IZodSchemaType> CreateConfigurationBuilderThroughReflection()
	{
		var configurationBuilder = LoadConfigurationBuilder();
		AddDefaultSubstitutions(configurationBuilder);
		
		return configurationBuilder;
	}

	private static void AddDefaultSubstitutions(ZodSchemaGeneratorConfigurationBuilder configurationBuilder)
	{
		configurationBuilder.Substitute<int, NumberZodSchemaType>();
		configurationBuilder.Substitute<float, NumberZodSchemaType>();
		configurationBuilder.Substitute<double, NumberZodSchemaType>();
		configurationBuilder.Substitute<decimal, NumberZodSchemaType>();
		configurationBuilder.Substitute<string, StringZodSchemaType>();
	}

	private static ZodSchemaGeneratorConfigurationBuilder LoadConfigurationBuilder()
	{
		var method = GetMethodInfoFromCsprojFile();
		if (method.GetParameters().Any())
		{
			throw new InvalidOperationException("Configuration method must not have any parameters.");
		}

		if (method.ReturnType != typeof(ZodSchemaGeneratorConfigurationBuilder))
		{
			throw new InvalidOperationException("Configuration method must return a ZodSchemaGeneratorConfigurationBuilder.");
		}

		var result = method.Invoke(null, null);
		if (result is not ZodSchemaGeneratorConfigurationBuilder builder)
		{
			throw new InvalidOperationException("Configuration method must return a ZodSchemaGeneratorConfigurationBuilder.");
		}

		return builder;
	}
	
	private static MethodInfo GetMethodInfoFromCsprojFile()
	{
		var methodName = GetMethodNameFromCsprojFile();
		var type = Type.GetType(methodName);
		if (type is null)
		{
			throw new Exception($"Could not find type {methodName} as specified in the csproj file");
		}

		return type.GetMethod("ConfigureSchemaGenerator") ?? throw new Exception($"Could not find method ConfigureSchemaGenerator on type {methodName} as specified in the csproj file");
	}
	
	private static string GetMethodNameFromCsprojFile()
	{
		// var csprojFileContent = File.ReadAllText(csprojFile);
		// var schemaGeneratorMethodFullName = Regex.Match(csprojFileContent, @"<SchemaGeneratorMethodFullName>(.*)</SchemaGeneratorMethodFullName>").Groups[1].Value;
		// return schemaGeneratorMethodFullName;

		return "TestingApp.SchemaGeneratorConfiguration.Configure";
	}
}