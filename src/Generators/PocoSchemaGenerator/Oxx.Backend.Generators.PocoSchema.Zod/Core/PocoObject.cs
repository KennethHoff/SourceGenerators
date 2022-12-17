using System.Reflection;

namespace Oxx.Backend.Generators.PocoSchema.Zod.Core;

public record struct PocoObject(string Name, IEnumerable<PropertyInfo> Properties);
