using AnotherProject;
using Oxx.Backend.Generators.PocoSchema.Core.Attributes;

namespace TestingApp.Models;

[SchemaObject]
internal abstract class MoleculeTest
{
	public required PocoFromAnotherProject PocoFromAnotherMother { get; init; }
	public required PocoFromAnotherProject? PocoFromAnotherNullable { get; init; }

	public required Gender Gender { get; init; }
}
