using AnotherProject;
using Oxx.Backend.Generators.PocoSchema.Core.Attributes;
using TestingApp.SchemaTypes;

namespace TestingApp.Models;

[PocoObject]
internal readonly record struct Role(string Name);

[PocoObject]
internal sealed class User
{
	public required Name Name { get; init; }
	public required string Email { get; init; }
	public required string Password { get; init; }
	
	public IEnumerable<Role> Roles { get; init; } = Enumerable.Empty<Role>();
}

[PocoObject]
internal readonly record struct Name(string Firstname, string? MiddleName, string Lastname);


[PocoObject]
internal abstract class Person
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
	
	public IReadOnlyCollection<User> RelatedUsers { get; init; } = Array.Empty<User>();
	public IReadOnlyCollection<User?> RelatedUsersNullableUnderlying { get; init; } = Array.Empty<User?>();
	public IReadOnlyCollection<User?>? RelatedUsersNullableUnderlyingNullable { get; init; } = Array.Empty<User?>();
	
	public int Int { get; init; }
	public int? IntNullable { get; init; }
	
	public Guid Guid { get; init; }
	public Guid? GuidNullable { get; init; }
	
	public PersonId PersonId { get; init; }
	public PersonId? PersonIdNullable { get; init; }

	public required PocoFromAnotherProject PocoFromAnotherMother { get; init; }
	public PocoFromAnotherProject? PocoFromAnotherNullable { get; init; }

	public Gender Gender { get; init; }

	private string Private { get; init; } = string.Empty;
	internal string Internal { get; init; } = string.Empty;
	protected string Protected { get; init; } = string.Empty;

	protected abstract string ProtectedAbstract { get; init; }
	

	// All of the following should be ignored as they are either:
	// - not a property (field, method, etc.)
	// - not an instance property (static)
	// - has [PocoPropertyIgnore] attribute

	[PocoPropertyIgnore]
	public string Ignored { get; init; } = string.Empty;
	
	public static string PublicStatic { get; set; } = string.Empty;
	private static string PrivateStatic { get; set; } = string.Empty;
	internal static string InternalStatic { get; set; } = string.Empty;
	protected static string ProtectedStatic { get; set; } = string.Empty;

	private readonly string PrivateReadonlyField = string.Empty;
	internal readonly string InternalReadonlyField = string.Empty;
	public readonly string PublicReadonlyField = string.Empty;
	protected readonly string ProtectedReadonlyField = string.Empty;
	private string PrivateField = string.Empty;
	internal string InternalField = string.Empty;
	public string PublicField = string.Empty;
	protected string ProtectedField = string.Empty;
	
	private static readonly string PrivateStaticReadonlyField = string.Empty;
	internal static readonly string InternalStaticReadonlyField = string.Empty;
	public static readonly string PublicStaticReadonlyField = string.Empty;
	protected static readonly string ProtectedStaticReadonlyField = string.Empty;
	private static string PrivateStaticField = string.Empty;
	internal static string InternalStaticField = string.Empty;
	public static string PublicStaticField = string.Empty;
	protected static string ProtectedStaticField = string.Empty;
	
	
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

public enum Gender
{
	Male,
	Female,
	Other,
}