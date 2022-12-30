using System.Reflection;
using Oxx.Backend.Generators.PocoSchema.Core.Models.Pocos.Contracts;

namespace Oxx.Backend.Generators.PocoSchema.Core.Logic.PocoExtraction;

public interface IPocoStructureExtractor
{
	IPocoStructure Get<T>()
		=> Get(typeof(T));
	IReadOnlyCollection<IPocoStructure> GetAllFromAssembly(Assembly assembly)
		=> GetAllFromAssemblies(new[]
		{
			assembly,
		});

	IReadOnlyCollection<IPocoStructure> GetAllFromAssemblyContaining<T>()
		=> GetAllFromAssembly(typeof(T).Assembly);
	IReadOnlyCollection<IPocoStructure> GetAllFromAssemblies(IReadOnlyCollection<Assembly> assemblies);
	IPocoStructure Get(Type type);
	IReadOnlyCollection<IPocoStructure> Get(IEnumerable<Type> types);
}