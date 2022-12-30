using System.Reflection;
using System.Runtime.CompilerServices;
using Oxx.Backend.Generators.PocoSchema.Core.Models.Types;

namespace Oxx.Backend.Generators.PocoSchema.Core.Extensions;

public static class TypeExtensions
{
	public static IReadOnlyCollection<EnumValue> GetEnumValues(this Type enumType)
	{
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
	
	public static IEnumerable<SchemaMemberInfo> GetValidSchemaMembers(this Type type)
	{
		const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
		IEnumerable<MemberInfo> properties = type.GetProperties(bindingFlags);
		IEnumerable<MemberInfo> fields = type.GetFields(bindingFlags);

		return properties.Concat(fields)
			.Where(x => x.GetCustomAttribute<CompilerGeneratedAttribute>() is null)
			.Select(x => new SchemaMemberInfo(x))
			.Where(x => x.IsIgnored is false);
	}
}

public readonly record struct EnumValue(string Name, int Value);
