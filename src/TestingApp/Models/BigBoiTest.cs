using AnotherProject;
using Oxx.Backend.Generators.PocoSchema.Core.Attributes;
using TestingApp.SchemaTypes;

namespace TestingApp.Models;

[SchemaObject]
internal abstract class BigBoiTest
{
	public required IEnumerable<string> Names { get; init; } = Enumerable.Empty<string>();
	public required IEnumerable<string?> NamesNullableUnderlying { get; init; } = Enumerable.Empty<string?>();
	public required IEnumerable<string?>? NamesNullableUnderlyingNullable { get; init; } = Enumerable.Empty<string?>();
	
	public required IReadOnlyCollection<string> ReadonlyNamesCollection { get; init; } = Array.Empty<string>();
	public required IReadOnlyCollection<string?> ReadonlyNamesCollectionNullableUnderlying { get; init; } = Array.Empty<string?>();
	public required IReadOnlyCollection<string?>? ReadonlyNamesCollectionNullableUnderlyingNullable { get; init; } = Array.Empty<string?>();
	
	public required IReadOnlyList<string> ReadonlyNamesList { get; init; } = Array.Empty<string>();
	public required IReadOnlyList<string?> ReadonlyNamesListNullableUnderlying { get; init; } = Array.Empty<string?>();
	public required IReadOnlyList<string?>? ReadonlyNamesListNullableUnderlyingNullable { get; init; } = Array.Empty<string?>();
	
	public required ICollection<string> NamesCollection { get; init; } = Array.Empty<string>();
	public required ICollection<string?> NamesCollectionNullableUnderlying { get; init; } = Array.Empty<string?>();
	public required ICollection<string?>? NamesCollectionNullableUnderlyingNullable { get; init; } = Array.Empty<string?>();
	
	public required IList<string> NamesList { get; init; } = Array.Empty<string>();
	public required IList<string?> NamesListNullableUnderlying { get; init; } = Array.Empty<string?>();
	public required IList<string?>? NamesListNullableUnderlyingNullable { get; init; } = Array.Empty<string?>();
	
	public required List<string> ListNames { get; init; } = new List<string>();
	public required List<string?> ListNamesNullableUnderlying { get; init; } = new List<string?>();
	public required List<string?>? ListNamesNullableUnderlyingNullable { get; init; } = new List<string?>();
	
	public required IEnumerable<Guid> Guids { get; init; } = Enumerable.Empty<Guid>();
	public required IEnumerable<Guid?> GuidsNullableUnderlying { get; init; } = Enumerable.Empty<Guid?>();
	public required IEnumerable<Guid?>? GuidsNullableUnderlyingNullable { get; init; } = Enumerable.Empty<Guid?>();
	
	public required List<Guid> ListGuids { get; init; } = new List<Guid>();
	public required List<Guid?> ListGuidsNullableUnderlying { get; init; } = new List<Guid?>();
	public required List<Guid?>? ListGuidsNullableUnderlyingNullable { get; init; } = new List<Guid?>();

	public required IReadOnlyCollection<User> RelatedUsers { get; init; } = Array.Empty<User>();
	public required IReadOnlyCollection<User?> RelatedUsersNullableUnderlying { get; init; } = Array.Empty<User?>();
	public required IReadOnlyCollection<User?>? RelatedUsersNullableUnderlyingNullable { get; init; } = Array.Empty<User?>();
	
	public required IReadOnlyCollection<Gender> Genders { get; init; }

	public required int Int { get; init; }
	public required int? IntNullable { get; init; }
	
	public required Guid Guid { get; init; }
	public required Guid? GuidNullable { get; init; }
	
	public required PersonId PersonId { get; init; }
	public required PersonId? PersonIdNullable { get; init; }
	
	public required PocoFromAnotherProject PocoFromAnotherMother { get; init; }
	public required PocoFromAnotherProject? PocoFromAnotherNullable { get; init; }
	
	public required Gender Gender { get; init; }
	
	private string Private { get; init; } = string.Empty;
	internal required string Internal { get; init; } = string.Empty;
	protected string Protected { get; init; } = string.Empty;
	
	protected abstract string ProtectedAbstract { get; init; }
	
	
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



	public required int[] ArrayInts { get; init; } = Array.Empty<int>();
	public required int?[] ArrayIntsNullableUnderlying { get; init; } = Array.Empty<int?>();
	public required int?[]? ArrayIntsNullableUnderlyingNullable { get; init; } = Array.Empty<int?>();
	
	public required string[] ArrayNames { get; init; } = Array.Empty<string>();
	public required string?[] ArrayNamesNullableUnderlying { get; init; } = Array.Empty<string?>();
	public required string?[]? ArrayNamesNullableUnderlyingNullable { get; init; } = Array.Empty<string?>();
	
	public required IEnumerable<ClampedNumber> ClampedNumbers { get; init; } = Enumerable.Empty<ClampedNumber>();

	// All of the following should be ignored as they are either:
	// - not a property or a field (methods, events, etc.)
	// - not an instance property (static)
	// - has [SchemaMemberIgnore] attribute

	[SchemaMemberIgnore]
	public required string Ignored { get; init; } = string.Empty;
	
	public static string PublicStatic { get; set; } = string.Empty;
	private static string PrivateStatic { get; set; } = string.Empty;
	internal static string InternalStatic { get; set; } = string.Empty;
	protected static string ProtectedStatic { get; set; } = string.Empty;
}