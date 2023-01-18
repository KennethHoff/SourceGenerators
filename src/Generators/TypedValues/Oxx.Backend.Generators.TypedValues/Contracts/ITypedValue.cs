namespace Oxx.Backend.Generators.TypedValues.Contracts;

public interface ITypedValue<TSelf, in TUnderlyingType> : IParsable<TSelf>
	where TSelf : ITypedValue<TSelf, TUnderlyingType>
	where TUnderlyingType : IComparable<TUnderlyingType>, IEquatable<TUnderlyingType>
{
	abstract static TSelf From(TUnderlyingType underlyingValue);
}