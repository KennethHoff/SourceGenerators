using Oxx.Backend.Generators.PocoSchema.Core.Models.Types;

namespace Oxx.Backend.Generators.PocoSchema.Core.Models.Schemas.Contracts;

public interface IAtomicSchema : ISchema
{ }

public interface IMolecularSchema<TSchema> : ISchema
	where TSchema : ISchema
{
	IDictionary<SchemaMemberInfo, TSchema> SchemaDictionary { get; }
}
