namespace Oxx.Backend.Generators.PocoSchema.Zod.Core;

// TODO: Move all of the stuff in `Core` subdirectory into its own project and package.
// I originally did it like that, but I had a lot of trouble getting it to work
// Besides, it's not necessary to have it in a separate package yet,
// as it's only one implementation so far
public interface ISchema
{
	IEnumerable<FileInformation> GenerateFileContent(IEnumerable<PocoObject> pocoObjects);
}

public record struct FileInformation(string Name, string Content);