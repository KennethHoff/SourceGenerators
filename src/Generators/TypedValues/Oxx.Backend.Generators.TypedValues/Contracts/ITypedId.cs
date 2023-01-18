namespace Oxx.Backend.Generators.TypedValues.Contracts;

public interface ITypedId<TSelf> : ITypedValue<TSelf, Guid>
	where TSelf : ITypedId<TSelf>
{ }
