using System.Reflection;
using Oxx.Backend.Generators.PocoSchema.Core.Configuration.Events;
using Oxx.Backend.Generators.PocoSchema.Core.Models.Schemas.Contracts;

namespace Oxx.Backend.Generators.PocoSchema.Core.Configuration;

public interface ISchemaConfiguration<TSchemaType, TSchemaEventConfiguration> : ISchemaConfiguration
	where TSchemaType : ISchema
	where TSchemaEventConfiguration : ISchemaEventConfiguration
{
	TSchemaEventConfiguration Events { get; }
	string FileExtension { get; }
	string FileExtensionInfix { get; }
	sealed string FullFileExtension => $"{FileExtensionInfix}{FileExtension}";
}

public interface ISchemaConfiguration
{
	IEnumerable<Assembly> Assemblies { get; }
	bool DeleteFilesOnStart { get; }
	string OutputDirectory { get; }
	string SchemaFileNameFormat { get; }
	string SchemaNamingFormat { get; }
	string SchemaTypeNamingFormat { get; }
}
