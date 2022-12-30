using Oxx.Backend.Generators.PocoSchema.Core.Models.Files;

namespace Oxx.Backend.Generators.PocoSchema.Core;

public interface ISchemaFileCreator
{
	Task CreateFilesAsync(IEnumerable<FileInformation> fileInformations);
	Task CreateFileAsync(FileInformation fileInformation);
}