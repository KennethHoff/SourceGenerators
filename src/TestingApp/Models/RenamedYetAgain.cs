using Oxx.Backend.Generators.PocoSchema.Zod.Core;

namespace TestingApp.Models;

[PocoObject]
internal sealed class RenamedYetAgain
{
	public string Name { get; init; } = string.Empty;
	public Guid Id { get; init; }
}
