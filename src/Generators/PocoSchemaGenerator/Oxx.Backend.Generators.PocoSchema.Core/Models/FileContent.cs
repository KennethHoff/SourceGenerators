namespace Oxx.Backend.Generators.PocoSchema.Core.Models;

public readonly record struct FileContent(string Value)
{
	public static readonly FileContent None = new()
	{
		Value = string.Empty,
	};
	
	// Implicit conversion from FileContent to string
	public static implicit operator string(FileContent fileContent) => fileContent.Value;

	public override string ToString()
		=> Value; 
}
