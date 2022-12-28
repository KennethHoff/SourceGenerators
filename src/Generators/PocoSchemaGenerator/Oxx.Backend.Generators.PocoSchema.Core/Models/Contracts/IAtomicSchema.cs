using System.Reflection;

namespace Oxx.Backend.Generators.PocoSchema.Core.Models.Contracts;

public interface IAtomicSchema : ISchema
{ }

public interface IMolecularSchema<TSchema> : ISchema
	where TSchema : ISchema
{
	IDictionary<PropertyInfo, TSchema> SchemaDictionary { get; }
}
