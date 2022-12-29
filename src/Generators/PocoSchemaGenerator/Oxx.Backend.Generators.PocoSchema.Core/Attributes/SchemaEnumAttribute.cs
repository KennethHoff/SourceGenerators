namespace Oxx.Backend.Generators.PocoSchema.Core.Attributes;

/// <summary>
///		This Attribute is used to mark an Enum for schema generation.
/// </summary>
[AttributeUsage(AttributeTargets.Enum, Inherited = false)]
public class SchemaEnumAttribute : SchemaTypeAttribute
{ }