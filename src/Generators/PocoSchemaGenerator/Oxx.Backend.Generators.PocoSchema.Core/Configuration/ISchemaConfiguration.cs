using System.Reflection;
using Oxx.Backend.Generators.PocoSchema.Core.Configuration.Events;

namespace Oxx.Backend.Generators.PocoSchema.Core.Configuration;

public interface ISchemaConfiguration<TSchemaType, TSchemaEventConfiguration> : ISchemaConfiguration
	where TSchemaType : ISchemaType
	where TSchemaEventConfiguration : ISchemaEventConfiguration
{
	TSchemaEventConfiguration Events { get; }
}

public interface ISchemaConfiguration
{
	IEnumerable<Assembly> Assemblies { get; }
	string OutputDirectory { get; }
	bool DeleteFilesOnStart { get; }
	string PropertyNamingConvention { get; }
	string PropertyTypeNamingConvention { get; }
}