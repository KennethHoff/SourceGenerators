using Oxx.Backend.Generators.PocoSchema.Core.Attributes;

namespace TestingApp.Models;

[SchemaObject]
internal sealed class User
{
	public required Name Name { get; init; }
	public required string Email { get; init; }
	public required string Password { get; init; }
	public required Gender Gender { get; init; }

	public IEnumerable<Role> Roles { get; init; } = Enumerable.Empty<Role>();
}