namespace Oxx.Backend.Generators.PocoSchema.Core.Attributes;

/// <summary>
///     This attribute is used to mark various structures(classes, interfaces, enums, etc.) as a structure to be used in the schema generation.
///     This attribute is used to generate a schema for the structure.
/// </summary>
/// <remarks>
///     The schema generator only exports properties, not fields, and includes all properties regardless of their access modifiers, unless they are marked with the
///     <see cref="PocoPropertyIgnoreAttribute" /> attribute.
/// </remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface | AttributeTargets.Enum, Inherited = false)]
public class SchemaGenerationAttribute : Attribute
{ }
