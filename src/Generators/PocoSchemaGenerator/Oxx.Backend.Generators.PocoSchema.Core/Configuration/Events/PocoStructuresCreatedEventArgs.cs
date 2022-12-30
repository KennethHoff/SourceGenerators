using Oxx.Backend.Generators.PocoSchema.Core.Models.Pocos.Contracts;
using Oxx.Backend.Generators.PocoSchema.Core.Models.Types;

namespace Oxx.Backend.Generators.PocoSchema.Core.Configuration.Events;

public sealed class PocoStructuresCreatedEventArgs : EventArgs
{
	public readonly IReadOnlyCollection<IPocoStructure> PocoStructures;
	public readonly IReadOnlyCollection<UnsupportedType> UnsupportedTypes;

	public PocoStructuresCreatedEventArgs(IReadOnlyCollection<IPocoStructure> pocoStructures, IReadOnlyCollection<UnsupportedType> unsupportedTypes)
	{
		PocoStructures = pocoStructures;
		UnsupportedTypes = unsupportedTypes;
	}
}