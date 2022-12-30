using Oxx.Backend.Generators.PocoSchema.Core.Attributes;

namespace Oxx.Backend.Generators.PocoSchema.Core.Models.Types;

public readonly record struct TypeSupport(IDictionary<SchemaTypeAttribute, List<Type>> Supported, ICollection<UnsupportedType> Unsupported);