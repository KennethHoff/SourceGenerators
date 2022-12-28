namespace Oxx.Backend.Generators.PocoSchema.Core.Models;

public readonly record struct FileName(string Value)
{
	public static readonly FileName None = new(string.Empty);

	#region Overrides

	public override string ToString()
		=> Value;

	#endregion

	// Implicit conversion from FileName to string
	public static implicit operator string(FileName fileName)
		=> fileName.Value;
}
