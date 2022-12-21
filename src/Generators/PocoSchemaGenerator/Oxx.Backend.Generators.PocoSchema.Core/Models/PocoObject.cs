using System.Reflection;
using Oxx.Backend.Generators.PocoSchema.Core.Configuration;

namespace Oxx.Backend.Generators.PocoSchema.Core.Models;

public readonly record struct PocoObject(Type Type, IEnumerable<PropertyInfo> Properties)
{
	public string GetSchemaName(ISchemaConfiguration configuration)
		=> string.Format(configuration.SchemaNamingFormat, Type.Name);
	
	public string Name => Type.Name;

	public string GetSchemaTypeName(ISchemaConfiguration configuration)
		=> string.Format(configuration.SchemaTypeNamingFormat, Type.Name);

	public string GetFileName(ISchemaConfiguration zodSchemaConfiguration)
		=> string.Format(zodSchemaConfiguration.SchemaFileNameFormat, Type.Name);
}