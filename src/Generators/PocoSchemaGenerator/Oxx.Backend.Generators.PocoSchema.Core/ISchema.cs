namespace Oxx.Backend.Generators.PocoSchema.Core;

public interface ISchema
{
	IEnumerable<FileInformation> GenerateFileContent(IEnumerable<PocoObject> pocoObjects);
}

public record struct FileInformation(string Name, string Content);