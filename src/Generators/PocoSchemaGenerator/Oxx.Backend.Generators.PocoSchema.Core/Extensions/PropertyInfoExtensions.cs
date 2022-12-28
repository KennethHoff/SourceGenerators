using System.Reflection;

namespace Oxx.Backend.Generators.PocoSchema.Core.Extensions;

public static class PropertyInfoExtensions
{
	private static readonly NullabilityInfoContext NullabilityInfoContext = new();

	public static bool IsNullable(this PropertyInfo p)
	{
		var nullabilityInfo = NullabilityInfoContext.Create(p);
		return nullabilityInfo.WriteState is NullabilityState.Nullable;
	}
}
