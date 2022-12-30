using Oxx.Backend.Generators.PocoSchema.Core.Attributes;

namespace TestingApp.Models;

[SchemaObject]
internal abstract class FieldyBoi
{
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