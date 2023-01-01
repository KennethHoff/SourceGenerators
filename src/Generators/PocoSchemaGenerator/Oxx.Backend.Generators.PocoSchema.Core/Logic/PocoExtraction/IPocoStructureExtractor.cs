using Oxx.Backend.Generators.PocoSchema.Core.Models.Pocos.Contracts;

namespace Oxx.Backend.Generators.PocoSchema.Core.Logic.PocoExtraction;

public interface IPocoStructureExtractor
{
	IReadOnlyCollection<IPocoStructure> GetAll();
	IReadOnlyCollection<IPocoStructure> Get(IEnumerable<Type> requestedTypes, bool includeDependencies = true);
}