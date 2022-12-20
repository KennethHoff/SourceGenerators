namespace Oxx.Backend.Generators.PocoSchema.Core.Extensions;

public static class StringExtensions
{
	public static string ToCamelCaseInvariant(this string value)
	{
		if (string.IsNullOrEmpty(value))
		{
			return value;
		}

		if (value.Length == 1)
		{
			return value.ToLowerInvariant();
		}

		return char.ToLowerInvariant(value[0]) + value.Substring(1);
	}

	public static string JoinWithNewLine(this IEnumerable<string> strings)
		=> string.Join(Environment.NewLine, strings);
}

