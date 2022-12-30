using Oxx.Backend.Generators.PocoSchema.Core.Models.Pocos;
using Oxx.Backend.Generators.PocoSchema.Core.Models.Pocos.Contracts;

namespace Oxx.Backend.Generators.PocoSchema.Core.Attributes;

/// <summary>
///     This Attribute is used to mark an Enum for schema generation.
/// </summary>
[AttributeUsage(AttributeTargets.Enum)]
public class SchemaEnumAttribute : SchemaTypeAttribute<PocoEnum>
{ }
