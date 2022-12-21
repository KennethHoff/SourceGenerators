using System.Reflection;
using Oxx.Backend.Generators.PocoSchema.Core.Configuration;

namespace Oxx.Backend.Generators.PocoSchema.Core.Models;

public record struct PocoObject(BaseName Name, IEnumerable<PropertyInfo> Properties);

public record struct BaseName(string Value)
{
	public string GetPropertyName(ISchemaConfiguration configuration)
		=> string.Format(configuration.PropertyNamingConvention, Value);
	
	public string GetPropertyTypeName(ISchemaConfiguration configuration)
		=> string.Format(configuration.PropertyTypeNamingConvention, Value);

	public override string ToString()
		=> Value;
}
