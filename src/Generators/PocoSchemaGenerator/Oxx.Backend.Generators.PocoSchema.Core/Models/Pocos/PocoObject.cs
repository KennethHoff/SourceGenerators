using Oxx.Backend.Generators.PocoSchema.Core.Models.Pocos.Contracts;
using Oxx.Backend.Generators.PocoSchema.Core.Models.Types;

namespace Oxx.Backend.Generators.PocoSchema.Core.Models.Pocos;

public record class PocoObject(Type Type, IEnumerable<SchemaMemberInfo> SchemaMembers) : IPocoStructure
{
	public string Name => Type.Name;
}