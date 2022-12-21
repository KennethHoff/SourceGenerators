using Oxx.Backend.Generators.PocoSchema.Core;

namespace TestingApp.Models;

[PocoObject]
internal sealed class Person : IPersonAge, IPersonName, IPersonId
{
	public int Age { get; init; }

	public string Name { get; init; } = string.Empty;
	public Guid Id { get; init; }
}

public interface IPersonAge
{
	int Age { get; init; }
}

public interface IPersonName
{
	string Name { get; init; }
}

public interface IPersonId
{
	Guid Id { get; init; }
}