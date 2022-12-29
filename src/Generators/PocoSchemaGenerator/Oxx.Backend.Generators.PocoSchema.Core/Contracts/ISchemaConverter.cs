using Oxx.Backend.Generators.PocoSchema.Core.Models.File;
using Oxx.Backend.Generators.PocoSchema.Core.Models.Poco.Contracts;

namespace Oxx.Backend.Generators.PocoSchema.Core.Contracts;

public interface ISchemaConverter
{
	IEnumerable<FileInformation> GenerateFileContent(IReadOnlyCollection<IPocoStructure> pocoStructures);
}
