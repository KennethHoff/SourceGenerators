using Oxx.Backend.Generators.PocoSchema.Core;

namespace TestingApp.Models;

[PocoObject]
internal sealed class NewPoco
{
	public NoobaTron NoobaTron { get; init; } = new();
}

[PocoObject]
internal sealed class EvenNewerPoco
{
	public required NoobaTron NoobaTron { get; init; }
	public required NewPoco NewPoco { get; init; }
	public required NewPoco NewPoco2 { get; init; }
	public required NewPoco NewPoco3 { get; init; }
	public required NewPoco NewPoco4 { get; init; }
}