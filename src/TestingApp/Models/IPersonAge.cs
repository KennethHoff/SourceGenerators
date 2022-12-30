using TestingApp.SchemaTypes;

namespace TestingApp.Models;

public interface IPersonAge
{
	ClampedNumber Age { get; init; }
}