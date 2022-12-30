using Oxx.Backend.Generators.PocoSchema.Core.Models.Pocos.Contracts;

namespace Oxx.Backend.Generators.PocoSchema.Core.Attributes;

public abstract class SchemaTypeAttribute : Attribute
{
	public abstract Type UnderlyingType { get; }
}

/// <summary>
///     Base class for all Schema Attributes that can be applied to a structure (class, struct, interface, enum, etc.. - anything that can be a type)
/// </summary>
public abstract class SchemaTypeAttribute<TPocoStructure> : SchemaTypeAttribute
	where TPocoStructure: IPocoStructure
{
	public override Type UnderlyingType => typeof(TPocoStructure);
}