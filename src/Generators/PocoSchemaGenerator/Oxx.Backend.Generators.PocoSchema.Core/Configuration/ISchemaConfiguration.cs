using System.Reflection;
using Oxx.Backend.Generators.PocoSchema.Core.Configuration.Events;
using Oxx.Backend.Generators.PocoSchema.Core.Models.Contracts;

namespace Oxx.Backend.Generators.PocoSchema.Core.Configuration;

public interface ISchemaConfiguration<TSchemaType, TSchemaEventConfiguration> : ISchemaConfiguration
	where TSchemaType : IAtomicSchema
	where TSchemaEventConfiguration : ISchemaEventConfiguration
{
	TSchemaEventConfiguration Events { get; }
}

public interface ISchemaConfiguration
{
	IEnumerable<Assembly> Assemblies { get; }
	string OutputDirectory { get; }
	bool DeleteFilesOnStart { get; }
	string SchemaNamingFormat { get; }
	string SchemaTypeNamingFormat { get; }
	string SchemaFileNameFormat { get; }
}