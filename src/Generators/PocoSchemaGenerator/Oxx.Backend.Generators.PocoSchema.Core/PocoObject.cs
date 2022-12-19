using System.Reflection;

namespace Oxx.Backend.Generators.PocoSchema.Core;

public record struct PocoObject(string Name, IEnumerable<PropertyInfo> Properties);
