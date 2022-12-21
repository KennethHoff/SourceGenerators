using Oxx.Backend.Generators.PocoSchema.Core;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Custom;

namespace TestingApp.Models;

[PocoObject]
internal sealed class Person : IPersonAge, IPersonName, IPersonId
{
	public int Age { get; init; }

	public string Name { get; init; } = string.Empty;
	public PersonId Id { get; init; }
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
	PersonId Id { get; init; }
}

public readonly record struct PersonId(Guid Id)
{
	public override string ToString()
		=> Id.ToString();
}

public readonly record struct CeremonyId(Guid Id)
{
	public override string ToString()
		=> Id.ToString();
}