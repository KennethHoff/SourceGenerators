using Oxx.Backend.Generators.PocoSchema.Core.Models.Contracts;

namespace Oxx.Backend.Generators.PocoSchema.Core.Models;

public sealed class TypeSchemaDictionary<TSchemaType> : Dictionary<Type, TSchemaType>
	where TSchemaType : class, ISchema
{
	public TSchemaType? GetSchemaForType(Type propertyType)
	{
		if (TryGetValue(propertyType, out var schema))
		{
			return schema;
		}

		// Check if the base type is in the dictionary, the more specific type will be used
		var baseType = propertyType.BaseType;
		if (baseType != null && TryGetValue(baseType, out schema))
		{
			return schema;
		}

		// Check if any of the interfaces are in the dictionary, the more specific type will be used
		var interfaces = propertyType.GetInterfaces();
		var interfaceType = interfaces.FirstOrDefault(ContainsKey);
		if (interfaceType is not null)
		{
			return this[interfaceType];
		}

		return null;
	}

	public bool HasSchemaForType(Type propertyType)
	{
		if (ContainsKey(propertyType))
		{
			return true;
		}

		// Check if the base type is in the dictionary, the more specific type will be used
		var baseType = propertyType.BaseType;
		if (baseType != null && ContainsKey(baseType))
		{
			return true;
		}

		// Check if any of the interfaces are in the dictionary, the more specific type will be used
		var interfaces = propertyType.GetInterfaces();
		if (interfaces.Any(ContainsKey))
		{
			return true;
		}

		return false;
	}
}
