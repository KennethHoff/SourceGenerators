using Oxx.Backend.Generators.PocoSchema.Core.Models;

namespace Oxx.Backend.Generators.PocoSchema.Core;

public interface ISchemaConverter
{
	IEnumerable<FileInformation> GenerateFileContent(IEnumerable<PocoObject> pocoObjects);
}