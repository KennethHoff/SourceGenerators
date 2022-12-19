namespace Oxx.Backend.Generators.PocoSchema.Zod.Core;

internal static class StringExtensions
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
}

