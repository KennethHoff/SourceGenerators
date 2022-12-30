using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts.Models;

namespace Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;

public interface IAdditionalImportZodSchema : IZodSchema
{
	IEnumerable<ZodImport> AdditionalImports { get; }
	string AdditionalImportsString => string.Join(Environment.NewLine, AdditionalImports);
}