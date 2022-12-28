using AnotherProject;
using Oxx.Backend.Generators.PocoSchema.Core.Attributes;
using TestingApp.SchemaTypes;

namespace TestingApp.Models;

[PocoObject]
internal sealed class Person
{
	public ClampedNumber Age { get; init; }

	public string Name { get; init; } = string.Empty;
	public string? NullableName { get; init; } = string.Empty;
	public PersonId Id { get; init; }
	public PersonId? Id2 { get; init; }
	
	public IEnumerable<PersonId> Ids { get; init; } = Enumerable.Empty<PersonId>();
	public IEnumerable<PersonId?> Ids2 { get; init; } = Enumerable.Empty<PersonId?>();
	public IEnumerable<PersonId?>? Ids3 { get; init; } = Enumerable.Empty<PersonId?>();
	
	public IEnumerable<Guid> Guids { get; init; } = Enumerable.Empty<Guid>();
	public IEnumerable<Guid?> Guids2 { get; init; } = Enumerable.Empty<Guid?>();
	public IEnumerable<Guid?>? Guids3 { get; init; } = Enumerable.Empty<Guid?>();
	
	public IEnumerable<string> Names { get; init; } = Enumerable.Empty<string>();
	public IEnumerable<string?> Names2 { get; init; } = Enumerable.Empty<string?>();
	public IEnumerable<string?>? Names3 { get; init; } = Enumerable.Empty<string?>();
	
	public IReadOnlyCollection<string> Names4 { get; init; } = new List<string>();
	public ICollection<string> Names5 { get; init; } = new List<string>();
	public IList<string> Names6 { get; init; } = new List<string>();
	public List<string> Names7 { get; init; } = new List<string>();
	public List<string>? Names8 { get; init; } = new List<string>();
	public List<string?> Names9 { get; init; } = new List<string?>();
	public List<string?>? Names10 { get; init; } = new List<string?>();
	public string[] Names11 { get; init; } = new string[0];
	public string?[] Names12 { get; init; } = new string?[0];
	public string?[]? Names13 { get; init; } = new string?[0];

	public required PocoFromAnotherProject PocoFromAnotherMother { get; init; }
}

[PocoObject]
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