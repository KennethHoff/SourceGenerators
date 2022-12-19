using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Oxx.Backend.Generators.PocoSchema.Zod.Core;

public record struct PocoObject(string Name, List<IPropertySymbol> Properties)
{
	public static readonly PocoObject None = new(string.Empty, new());
}
