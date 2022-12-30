using System.Reflection;
using System.Text.RegularExpressions;
using Oxx.Backend.Generators.PocoSchema.Core.Configuration.Abstractions;
using Oxx.Backend.Generators.PocoSchema.Core.Configuration.Events;
using Oxx.Backend.Generators.PocoSchema.Core.Models.Schemas.Contracts;

namespace Oxx.Backend.Generators.PocoSchema.Core.Configuration;

public interface ISchemaConfiguration<TSchemaType, TSchemaEventConfiguration> : ISchemaConfiguration
	where TSchemaType : ISchema
	where TSchemaEventConfiguration : ISchemaEventConfiguration
{
	TSchemaEventConfiguration Events { get; }
}

public interface ISchemaConfiguration
{
	IEnumerable<Assembly> Assemblies { get; }
	FileDeletionMode FileDeletionMode { get; }
	DirectoryInfo OutputDirectory { get; }
	string SchemaFileNameFormat { get; }
	string SchemaNamingFormat { get; }
	string SchemaTypeNamingFormat { get; }
	string FileExtension { get; }
	string FileExtensionInfix { get; }
	sealed string FullFileExtension => $"{FileExtensionInfix}{FileExtension}";
	sealed Regex FullFileNamingRegex => new($"^{SchemaFileNameFormat.Replace("{0}", "(?<name>.*)")}{FullFileExtension}$");
}
