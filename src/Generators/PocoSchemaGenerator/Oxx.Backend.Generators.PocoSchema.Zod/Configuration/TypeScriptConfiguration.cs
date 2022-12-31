namespace Oxx.Backend.Generators.PocoSchema.Zod.Configuration;

public class TypeScriptConfiguration
{
	public string Alias { get; set; } = string.Empty;
	public bool Valid => string.IsNullOrWhiteSpace(Alias) is false;
}