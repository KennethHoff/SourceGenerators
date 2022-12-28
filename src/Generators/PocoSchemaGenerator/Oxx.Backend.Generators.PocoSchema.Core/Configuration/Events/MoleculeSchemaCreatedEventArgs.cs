using System.Reflection;
using Oxx.Backend.Generators.PocoSchema.Core.Models.Contracts;

namespace Oxx.Backend.Generators.PocoSchema.Core.Configuration.Events;

public sealed class MoleculeSchemaCreatedEventArgs : EventArgs
{
	public readonly Type Type;
	public readonly ISchema Schema;
	public readonly IReadOnlyCollection<PropertyInfo> InvalidProperties;

	public MoleculeSchemaCreatedEventArgs(Type type, ISchema schema, IReadOnlyCollection<PropertyInfo> invalidProperties)
	{
		Type = type;
		Schema = schema;
		InvalidProperties = invalidProperties;
	}
}
