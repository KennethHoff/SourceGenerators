using AnotherProject;
using Oxx.Backend.Generators.PocoSchema.Core.Attributes;
using TestingApp.SchemaTypes;

namespace TestingApp.Models;

[PocoObject]
internal sealed class Person : IPersonAge, IPersonName, IPersonId
{
	public ClampedNumber Age { get; init; }

	public string Name { get; init; } = string.Empty;
	public PersonId Id { get; init; }
	public PersonId? Id2 { get; init; }

	[PocoPropertyIgnore]
	public required PocoFromAnotherProject PocoFromAnotherMother { get; init; }
}

public interface IPersonAge
{
	ClampedNumber Age { get; init; }
}

public interface IPersonName
{
	string Name { get; init; }
}

public interface IPersonId
{
	PersonId Id { get; init; }
}

public readonly record struct PersonId(Guid Id)
{
	public override string ToString()
		=> Id.ToString();
}

public readonly record struct CeremonyId(Guid Id)
{
	public override string ToString()
		=> Id.ToString();
}