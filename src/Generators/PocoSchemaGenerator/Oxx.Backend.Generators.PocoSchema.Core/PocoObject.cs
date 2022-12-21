using System.Reflection;
using Oxx.Backend.Generators.PocoSchema.Core.Configuration;

namespace Oxx.Backend.Generators.PocoSchema.Core;

public record struct PocoObject(BaseName Name, IEnumerable<PropertyInfo> Properties);

public record struct BaseName(string Value)
{
	public string GetSchemaName(ISchemaConfiguration configuration)
		=> string.Format(configuration.SchemaNamingConvention, Value);
	
	public string GetSchemaTypeName(ISchemaConfiguration configuration)
		=> string.Format(configuration.SchemaTypeNamingConvention, Value);

	public override string ToString()
		=> Value;
}
