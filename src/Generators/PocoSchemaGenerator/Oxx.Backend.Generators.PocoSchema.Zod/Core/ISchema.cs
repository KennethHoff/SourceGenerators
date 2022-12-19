namespace Oxx.Backend.Generators.PocoSchema.Zod.Core;

public interface ISchema
{
	IEnumerable<FileInformation> GenerateFileContent(IEnumerable<PocoObject> pocoObjects);
}

public record struct FileInformation(string Name, string Content);