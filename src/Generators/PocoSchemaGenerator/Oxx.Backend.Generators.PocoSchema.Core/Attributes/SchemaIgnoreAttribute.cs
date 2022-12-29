namespace Oxx.Backend.Generators.PocoSchema.Core.Attributes;

/// <summary>
///     This attribute is used to ignore a field or property from the POCO.
/// </summary>
/// <remarks>
///     It is generally not recommended to use this attribute, as it is best to have a 1:1 mapping between the POCO and the mapped schema for ease of maintenance.
///     <br />
///     However, it may be necessary in a legacy codebase where the POCO is not well designed.
/// </remarks>
[AttributeUsage(AttributeTargets.Property)]
public class SchemaIgnoreAttribute : Attribute
{ }
