using System.Reflection;
using Oxx.Backend.Generators.PocoSchema.Core.Models.Poco.Contracts;

namespace Oxx.Backend.Generators.PocoSchema.Core.Models.Poco;

public record class PocoObject(System.Type ObjectType, IEnumerable<PropertyInfo> Properties) : IPocoStructure
{
	public string TypeName => ObjectType.Name;
}