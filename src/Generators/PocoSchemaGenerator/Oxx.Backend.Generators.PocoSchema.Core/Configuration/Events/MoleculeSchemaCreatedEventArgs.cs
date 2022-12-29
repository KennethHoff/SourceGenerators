using Oxx.Backend.Generators.PocoSchema.Core.Configuration.Events.Models;

namespace Oxx.Backend.Generators.PocoSchema.Core.Configuration.Events;

public sealed class MoleculeSchemaCreatedEventArgs : EventArgs
{
	public readonly CreatedSchemaInformation Information;

	public MoleculeSchemaCreatedEventArgs(CreatedSchemaInformation information)
	{
		Information = information;
	}
}
