using Oxx.Backend.Generators.PocoSchema.Core;

namespace TestingApp.Models;

[PocoObject]
internal sealed class NewPoco
{
	public NoobaTron NoobaTron { get; init; } = new();
}
