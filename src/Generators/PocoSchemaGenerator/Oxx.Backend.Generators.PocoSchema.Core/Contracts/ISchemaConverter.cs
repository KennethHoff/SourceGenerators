using Oxx.Backend.Generators.PocoSchema.Core.Models.Files;
using Oxx.Backend.Generators.PocoSchema.Core.Models.Pocos.Contracts;

namespace Oxx.Backend.Generators.PocoSchema.Core.Contracts;

public interface ISchemaConverter
{
	IEnumerable<FileInformation> GenerateFileContent(IReadOnlyCollection<IPocoStructure> pocoStructures);
}
