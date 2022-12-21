namespace Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;

public readonly record struct SchemaName(string Name)
{
	public static readonly SchemaName BuiltIn = new("__BuiltIn__");
	public override string ToString()
		=> Name;
}
