namespace Oxx.Backend.Generators.PocoSchema.Core.Models.Files;

public readonly record struct FileContent(string Value)
{
	public static readonly FileContent None = new()
	{
		Value = string.Empty,
	};

	#region Overrides

	public override string ToString()
		=> Value;

	#endregion

	// Implicit conversion from FileContent to string
	public static implicit operator string(FileContent fileContent)
		=> fileContent.Value;
}
