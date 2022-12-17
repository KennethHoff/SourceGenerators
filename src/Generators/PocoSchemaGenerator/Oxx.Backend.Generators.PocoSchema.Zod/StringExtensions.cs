namespace Oxx.Backend.Generators.PocoSchema.Zod;

public static class StringExtensions
{
	public static string JoinWithNewLine(this IEnumerable<string> strings)
		=> string.Join(Environment.NewLine, strings);
}
