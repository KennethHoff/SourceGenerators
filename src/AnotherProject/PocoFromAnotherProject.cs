using Oxx.Backend.Generators.PocoSchema.Core.Attributes;

namespace AnotherProject;

[PocoObject]
public class PocoFromAnotherProject
{
	public required string MyString { get; init; }
}
