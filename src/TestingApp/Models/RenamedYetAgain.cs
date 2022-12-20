using Oxx.Backend.Generators.PocoSchema.Core;

namespace TestingApp.Models;

[PocoObject]
internal sealed class RenamedYetAgain
{
	public string Name { get; init; } = string.Empty;
	public Guid Id { get; init; }
	
	public int Age { get; init; }
	
	public DateTime Date { get; init; }
	
	public bool IsTrue { get; init; }

	public bool? NullableBoolYikes { get; init; }
	public int? NullableInt { get; init; }
	
	public string? NullableString { get; init; }
	
	public DateOnly? NullableDateOnly { get; init; }
	public DateOnly DateOnly { get; init; }
}
