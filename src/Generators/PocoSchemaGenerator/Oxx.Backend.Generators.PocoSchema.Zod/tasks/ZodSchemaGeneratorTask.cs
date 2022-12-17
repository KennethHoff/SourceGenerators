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

	private ZodSchemaGeneratorConfigurationBuilder LoadConfigurationBuilder()
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
	
	private MethodInfo GetMethodInfoFromCsprojFile()
	{
		String fullNamespace = SchemaGeneratorConfigurationMethodFullNamespace;
		var assemblyName = SchemaGeneratorConfigurationMethodAssemblyName;
		Log.LogMessage("Assembly name: {0}", assemblyName);
		Log.LogMessage("Full namespace: {0}", fullNamespace);
		
		var taskAssembly = typeof(ZodSchemaGeneratorTask).Assembly.GetName();
		Log.LogMessage($"AppDomain base Dir {AppDomain.CurrentDomain.BaseDirectory}");
		Log.LogMessage($"CurrentWorkingDir {Directory.GetCurrentDirectory()}");
		Log.LogMessage($"** CodeBase for MyTask's assembly name: {taskAssembly.CodeBase}");
		
		
		var allAssemblies = AppDomain.CurrentDomain.GetAssemblies();
		Log.LogMessage("All assemblies: {0}", string.Join(Environment.NewLine, allAssemblies.Select(a => a.FullName)));
		
		var assembly = Assembly.Load(assemblyName);
		var type = assembly.GetType(fullNamespace);
		if (type is null)
		{
			throw new Exception($"Could not find type {fullNamespace} as specified in the csproj file");
		}

		return type.GetMethod("ConfigureSchemaGenerator") ?? throw new Exception($"Could not find method ConfigureSchemaGenerator on type {fullNamespace} as specified in the csproj file");
	}
}