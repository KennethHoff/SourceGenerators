using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts.Models;

namespace Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;

public interface IAdditionalImportZodSchema : IZodSchema
{
	IEnumerable<ZodImport> AdditionalImports { get; }
	string AdditionalImportsString
	{
		get
		{
			var nonEmptyImports = AdditionalImports.Where(x => x != ZodImport.None).ToArray();

			return nonEmptyImports.Any()
				? Environment.NewLine + string.Join(Environment.NewLine, nonEmptyImports)
				: string.Empty;
		}
	}
}