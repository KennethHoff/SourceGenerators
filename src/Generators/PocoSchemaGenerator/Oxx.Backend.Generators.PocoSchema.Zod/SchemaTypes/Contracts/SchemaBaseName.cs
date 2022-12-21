namespace Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;

public readonly record struct SchemaBaseName(string Name)
{
	public override string ToString()
		=> Name;
}
