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
	ZodSchemaConfiguration Configuration { get; }
	
	void SetConfiguration(ZodSchemaConfiguration configuration);
}

public interface IAdditionalImportZodSchema : IZodSchema
{
	IEnumerable<string> AdditionalImports { get; }
	string AdditionalImportsString => string.Join(Environment.NewLine, AdditionalImports);
}