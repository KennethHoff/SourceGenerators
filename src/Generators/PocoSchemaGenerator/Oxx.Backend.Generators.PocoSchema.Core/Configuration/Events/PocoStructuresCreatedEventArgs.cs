using Oxx.Backend.Generators.PocoSchema.Core.Models.Pocos.Contracts;

namespace Oxx.Backend.Generators.PocoSchema.Core.Configuration.Events;

public sealed class PocoStructuresCreatedEventArgs : EventArgs
{
	public readonly IPocoStructure[] PocoStructures;
	public readonly List<(Type Type, Exception Exception)> UnsupportedTypes;

	public PocoStructuresCreatedEventArgs(IPocoStructure[] pocoStructures, List<(Type Type, Exception Exception)> unsupportedTypes)
	{
		PocoStructures = pocoStructures;
		UnsupportedTypes = unsupportedTypes;
	}
}
