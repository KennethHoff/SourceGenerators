using Oxx.Backend.Generators.PocoSchema.Zod.Core;

namespace TestingApp.Models;

[PocoObject]
internal sealed class TestPoco
{
	public string Name { get; init; } = string.Empty;
}
