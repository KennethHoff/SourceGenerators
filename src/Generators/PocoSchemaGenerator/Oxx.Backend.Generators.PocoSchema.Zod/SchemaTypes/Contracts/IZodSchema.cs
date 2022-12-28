using System.Reflection;
using Oxx.Backend.Generators.PocoSchema.Core.Models.Contracts;
using Oxx.Backend.Generators.PocoSchema.Zod.Configuration;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts.Models;

namespace Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;

public interface IZodSchema : IPartialZodSchema
{
	SchemaDefinition SchemaDefinition { get; }
}

public interface IPartialZodSchema : ISchema
{
	SchemaBaseName SchemaBaseName { get; }
}

public interface IGenericZodSchema : IZodSchema
{
	ZodSchemaConfiguration Configuration { get; set; }
	PropertyInfo PropertyInfo { get; set; }
}

public interface IAdditionalImportZodSchema : IZodSchema
{
	IEnumerable<ZodImport> AdditionalImports { get; }
	string AdditionalImportsString => string.Join(Environment.NewLine, AdditionalImports);
}

public readonly record struct ZodImport(string SchemaName, string FilePath)
{
	public static readonly ZodImport None = new(string.Empty, string.Empty);

	#region Overrides

	public override string ToString()
		=> $$"""import { {{SchemaName}} } from "{{FilePath}}";""";

	#endregion
}
