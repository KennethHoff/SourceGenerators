using Oxx.Backend.Generators.PocoSchema.Core.Attributes;

namespace AnotherProject;

[SchemaObject]
public class PocoFromAnotherProject
{
	public required string MyString { get; init; }
}
