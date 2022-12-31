using Oxx.Backend.Generators.PocoSchema.Core.Models.Pocos.Contracts;

namespace Oxx.Backend.Generators.PocoSchema.Core.Models.Pocos;

public record class PocoAtom(Type Type) : IPocoStructure
{
	public string Name => Type.Name;
}
