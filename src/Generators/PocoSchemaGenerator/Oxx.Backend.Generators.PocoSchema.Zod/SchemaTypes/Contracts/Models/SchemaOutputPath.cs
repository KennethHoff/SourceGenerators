namespace Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;

public readonly record struct SchemaOutputPath(Uri Path)
{
	public static readonly SchemaOutputPath None = new(new Uri("__NONE__"));
	// Implicit conversion from SchemaOutputPath to Uri
	public static implicit operator Uri(SchemaOutputPath schemaOutputPath) => schemaOutputPath.Path;

	public override string ToString()
		=> Path.ToString();
}
