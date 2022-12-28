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

		return char.ToLowerInvariant(value[0]) + value[1..];
	}

	public static string TrimEnd(this string str, string value)
		=> str.EndsWith(value)
			? str[..^value.Length]
			: str;
}
