namespace Oxx.Backend.Generators.PocoSchema.Core.Models.Types;

public readonly record struct TypeSupport(TypeCollectionTypeDictionary Supported, ICollection<UnsupportedType> Unsupported);

public sealed class TypeCollectionTypeDictionary : Dictionary<Type, List<Type>> { }