﻿using Oxx.Backend.Generators.PocoSchema.Core.Models.Schema.Contracts;

namespace Oxx.Backend.Generators.PocoSchema.Core.Models.Type;

public sealed class TypeSchemaDictionary<TSchemaType> : Dictionary<System.Type, TSchemaType>
	where TSchemaType : class, ISchema
{
	public TSchemaType? GetSchemaForType(System.Type propertyType)
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

	public bool HasSchemaForType(System.Type propertyType)
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

	public void Update(System.Type type, TSchemaType updatedSchema)
	{
		if (ContainsKey(type))
		{
			this[type] = updatedSchema;
		}
		else
		{
			throw new InvalidOperationException($"Type {type} could not be updated: it does not exist in the dictionary.");
		}
	}
}