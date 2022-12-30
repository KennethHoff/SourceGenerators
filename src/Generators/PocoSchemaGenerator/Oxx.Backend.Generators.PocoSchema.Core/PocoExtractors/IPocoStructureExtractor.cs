using Oxx.Backend.Generators.PocoSchema.Core.Models.Pocos.Contracts;

namespace Oxx.Backend.Generators.PocoSchema.Core.PocoExtractors;

public interface IPocoStructureExtractor
{
	IPocoStructure Get<T>();
	IPocoStructure[] GetAll();
	IPocoStructure Get(Type type);
}