namespace Oxx.Backend.Generators.PocoSchema.Core.Attributes;

/// <summary>
///		This Attribute is used to mark an Enum for schema generation.
/// </summary>
[AttributeUsage(AttributeTargets.Enum, Inherited = false)]
public class SchemaEnumAttribute<TEnum> : SchemaEnumAttribute
	where TEnum : struct, Enum
{
	public override Type EnumType => typeof(TEnum);
}

public abstract class SchemaEnumAttribute : SchemaTypeAttribute
{
	public abstract Type EnumType { get; }
}