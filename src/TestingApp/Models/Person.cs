using System.Runtime.CompilerServices;
using AnotherProject;
using Oxx.Backend.Generators.PocoSchema.Core.Attributes;
using TestingApp.SchemaTypes;

namespace TestingApp.Models;

[PocoObject]
internal sealed class Person
{
	public IEnumerable<string> Names { get; init; } = Enumerable.Empty<string>();
	public IEnumerable<string?> NamesNullableUnderlying { get; init; } = Enumerable.Empty<string?>();
	public IEnumerable<string?>? NamesNullableUnderlyingNullable { get; init; } = Enumerable.Empty<string?>();
	
	public IReadOnlyCollection<string> ReadonlyNamesCollection { get; init; } = Array.Empty<string>();
	public IReadOnlyCollection<string?> ReadonlyNamesCollectionNullableUnderlying { get; init; } = Array.Empty<string?>();
	public IReadOnlyCollection<string?>? ReadonlyNamesCollectionNullableUnderlyingNullable { get; init; } = Array.Empty<string?>();

	public IReadOnlyList<string> ReadonlyNamesList { get; init; } = Array.Empty<string>();
	public IReadOnlyList<string?> ReadonlyNamesListNullableUnderlying { get; init; } = Array.Empty<string?>();
	public IReadOnlyList<string?>? ReadonlyNamesListNullableUnderlyingNullable { get; init; } = Array.Empty<string?>();
	
	public ICollection<string> NamesCollection { get; init; } = Array.Empty<string>();
	public ICollection<string?> NamesCollectionNullableUnderlying { get; init; } = Array.Empty<string?>();
	public ICollection<string?>? NamesCollectionNullableUnderlyingNullable { get; init; } = Array.Empty<string?>();
	
	public IList<string> NamesList { get; init; } = Array.Empty<string>();
	public IList<string?> NamesListNullableUnderlying { get; init; } = Array.Empty<string?>();
	public IList<string?>? NamesListNullableUnderlyingNullable { get; init; } = Array.Empty<string?>();
	
	public List<string> ListNames { get; init; } = new List<string>();
	public List<string?> ListNamesNullableUnderlying { get; init; } = new List<string?>();
	public List<string?>? ListNamesNullableUnderlyingNullable { get; init; } = new List<string?>();
	
	public string[] ArrayNames { get; init; } = Array.Empty<string>();
	public string?[] ArrayNamesNullableUnderlying { get; init; } = Array.Empty<string?>();
	public string?[]? ArrayNamesNullableUnderlyingNullable { get; init; } = Array.Empty<string?>();
	
	public IEnumerable<Guid> Guids { get; init; } = Enumerable.Empty<Guid>();
	public IEnumerable<Guid?> GuidsNullableUnderlying { get; init; } = Enumerable.Empty<Guid?>();
	public IEnumerable<Guid?>? GuidsNullableUnderlyingNullable { get; init; } = Enumerable.Empty<Guid?>();
	
	public List<Guid> ListGuids { get; init; } = new List<Guid>();
	public List<Guid?> ListGuidsNullableUnderlying { get; init; } = new List<Guid?>();
	public List<Guid?>? ListGuidsNullableUnderlyingNullable { get; init; } = new List<Guid?>();
	
	public int[] ArrayInts { get; init; } = Array.Empty<int>();
	public int?[] ArrayIntsNullableUnderlying { get; init; } = Array.Empty<int?>();
	public int?[]? ArrayIntsNullableUnderlyingNullable { get; init; } = Array.Empty<int?>();
	
	public int Int { get; init; }
	public int? IntNullable { get; init; }
	
	public Guid Guid { get; init; }
	public Guid? GuidNullable { get; init; }
	
	public PersonId PersonId { get; init; }
	public PersonId? PersonIdNullable { get; init; }

	public required PocoFromAnotherProject PocoFromAnotherMother { get; init; }
	public PocoFromAnotherProject? PocoFromAnotherNullable { get; init; }
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