using Oxx.Backend.Generators.PocoSchema.Core;

namespace AnotherProject;

[PocoObject]
public class PocoFromAnotherProject
{
	public required string MyString { get; init; }
}
