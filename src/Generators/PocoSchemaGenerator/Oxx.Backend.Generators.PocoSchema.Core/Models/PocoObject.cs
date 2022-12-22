using System.Reflection;

namespace Oxx.Backend.Generators.PocoSchema.Core.Models;

public readonly record struct PocoObject(Type Type, IEnumerable<PropertyInfo> Properties)
{
	public string TypeName => Type.Name;
}