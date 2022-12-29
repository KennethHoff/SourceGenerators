namespace Oxx.Backend.Generators.PocoSchema.Core.Extensions;

public static class TypeExtensions
{
	public static IReadOnlyCollection<EnumValue> GetEnumValues(this Type enumType)
	{
		if (!enumType.IsEnum)
		{
			throw new ArgumentException("Type must be an enum.", nameof(enumType));
		}
		
		var enumValues = Enum.GetValues(enumType);
		var enumNames = Enum.GetNames(enumType);
		var enumKeyValuePairs = new List<EnumValue>();
		for (var i = 0; i < enumValues.Length; i++)
		{
			var name = enumNames[i];
			var valueObject = enumValues.GetValue(i);
			var value = Convert.ToInt32(valueObject);

			enumKeyValuePairs.Add(new EnumValue(name, value));
		}

		return enumKeyValuePairs;
	}
}

public readonly record struct EnumValue(string Name, int Value);

