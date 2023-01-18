namespace Oxx.Backend.Generators.TypedValues.Generator;

internal static class Interfaces
{
	public static readonly string TypedValue =
		$$"""
		{{FileCreationHelpers.HeaderTemplate}}

		namespace {{FileCreationHelpers.RootNamespace}};

		public interface ITypedValue<TSelf, in TUnderlyingType> : IParsable<TSelf>
		    where TSelf : ITypedValue<TSelf, TUnderlyingType>
		    where TUnderlyingType : IComparable<TUnderlyingType>, IEquatable<TUnderlyingType>
		{
		    abstract static TSelf From(TUnderlyingType underlyingValue);
		}
		""";

	public static readonly string TypedId =
		$$"""
		{{FileCreationHelpers.HeaderTemplate}}

		namespace {{FileCreationHelpers.RootNamespace}};

		public interface ITypedId<TSelf> : ITypedValue<TSelf, Guid>
		    where TSelf : ITypedId<TSelf>
		{ }
		""";
}

