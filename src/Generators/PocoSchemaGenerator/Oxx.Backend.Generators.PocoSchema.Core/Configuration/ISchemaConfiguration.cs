using System.Reflection;
using System.Text.RegularExpressions;
using Oxx.Backend.Generators.PocoSchema.Core.Configuration.Abstractions;
using Oxx.Backend.Generators.PocoSchema.Core.Configuration.Events;

namespace Oxx.Backend.Generators.PocoSchema.Core.Configuration;

public interface ISchemaConfiguration<out TSchemaEvents, out TDirectoryOutputConfiguration> : ISchemaConfiguration
	where TSchemaEvents : ISchemaEvents
{
	TSchemaEvents Events { get; }
	TDirectoryOutputConfiguration DirectoryOutputConfiguration { get; }
}

public interface ISchemaConfiguration
{
	IEnumerable<Assembly> Assemblies { get; }
	FileDeletionMode FileDeletionMode { get; }
	string SchemaFileNameFormat { get; }
	string SchemaNamingFormat { get; }
	string SchemaTypeNamingFormat { get; }
	string FileExtension { get; }
	string FileExtensionInfix { get; }
	sealed string FullFileExtension => $"{FileExtensionInfix}{FileExtension}";
	sealed Regex FullFileNamingRegex => new($"^{SchemaFileNameFormat.Replace("{0}", "(?<name>.*)")}{FullFileExtension}$");
}