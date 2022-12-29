using Oxx.Backend.Generators.PocoSchema.Core.Configuration.Events.Models;

namespace Oxx.Backend.Generators.PocoSchema.Core.Configuration.Events;

public sealed class MoleculeSchemasCreatedEventArgs : EventArgs
{
	public readonly IReadOnlyCollection<CreatedSchemaInformation> Informations;

	public MoleculeSchemasCreatedEventArgs(IReadOnlyCollection<CreatedSchemaInformation> informations)
	{
		Informations = informations;
	}
}
