using System.Reflection;

namespace Oxx.Backend.Generators.PocoSchema.Zod.Core;

public abstract class SchemaGeneratorConfigurationBuilder
{
	internal readonly IList<Assembly> Assemblies = new List<Assembly>();
	internal string OutputDirectory = string.Empty;
	internal bool IsValid => !string.IsNullOrWhiteSpace(OutputDirectory) && Assemblies.Any();

	public void ResolveTypesFromAssemblyContaining<T>()
	{
		var assembly = typeof(T).GetTypeInfo().Assembly;

		ResolveTypesFromAssembly(assembly);
	}

	public void ResolveTypesFromAssemblies(params Assembly[] assemblies)
	{
		foreach (var assembly in assemblies)
		{
			ResolveTypesFromAssembly(assembly);
		}
	}

	public void ResolveTypesFromAssembly(Assembly assembly)
	{
		Assemblies.Add(assembly);
	}

	public void SetRootDirectory(string rootDirectory)
	{
		OutputDirectory = rootDirectory;
	}
}

public abstract class SchemaGeneratorConfigurationBuilder<TSchemaType> : SchemaGeneratorConfigurationBuilder
	where TSchemaType: class, ISchemaType
{
	internal readonly IDictionary<Type, TSchemaType> SchemaTypeDictionary = new Dictionary<Type, TSchemaType>();

	public abstract void Substitute<TPoco, TSubstitute>() where TSubstitute : TSchemaType, new();
}
