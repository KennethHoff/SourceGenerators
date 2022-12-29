using Oxx.Backend.Generators.PocoSchema.Core.Attributes;

namespace AnotherProject;

[SchemaGeneration]
public class PocoFromAnotherProject
{
	public required string MyString { get; init; }
}
