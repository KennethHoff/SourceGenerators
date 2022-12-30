namespace TestingApp.Models;

public readonly record struct PersonId(Guid Id)
{
	public override string ToString()
		=> Id.ToString();
}