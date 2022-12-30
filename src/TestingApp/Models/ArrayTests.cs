using Oxx.Backend.Generators.PocoSchema.Core.Attributes;

namespace TestingApp.Models;

[SchemaObject]
internal sealed class ArrayTests
{
	public required int[] IntArray { get; set; }
	public required int[]? NullableIntArray { get; set; }

	public required string[] StringArray { get; set; }
	public required string[]? NullableStringArray { get; set; }
}
