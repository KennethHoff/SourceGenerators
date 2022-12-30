using System.Diagnostics.CodeAnalysis;
using Namotion.Reflection;
using Oxx.Backend.Generators.PocoSchema.Core.Models.Schemas.Contracts;

namespace Oxx.Backend.Generators.PocoSchema.Core.Models.Types;

public sealed class TypeSchemaDictionary<TSchema> : Dictionary<Type, TSchema>
	where TSchema : class, ISchema
{
	public TSchema? GetMemberForMemberInfo(SchemaMemberInfo memberInfo)
		=> GetSchemaForType(memberInfo.Type);

	public TSchema? GetSchemaForType(Type propertyType)
	{
		if (TryGetValue(propertyType, out var schema))
		{
			return schema;
		}

		if (TryGetNonNullableTypeValue(propertyType, out var nonNullableType))
		{
			return nonNullableType;
		}

		if (TryGetBaseTypeValue(propertyType, out var baseType))
		{
			return baseType;
		}

		if (TryGetInterfaceTypeValue(propertyType, out var interfaceType))
		{
			return interfaceType;
		}

		return null;
	}

	public bool HasSchemaForMemberInfo(SchemaMemberInfo memberInfo)
		=> GetMemberForMemberInfo(memberInfo) != null;

	public bool HasSchemaForType(Type propertyType)
		=> GetSchemaForType(propertyType) != null;

	public void Update(Type type, TSchema updatedSchema)
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

	private bool TryGetBaseTypeValue(Type propertyType, [NotNullWhen(true)] out TSchema? returnType)
	{
		var baseType = propertyType.BaseType;
		if (baseType is null)
		{
			returnType = null;
			return false;
		}

		if (TryGetValue(baseType, out var schema))
		{
			returnType = schema;
			return true;
		}

		returnType = null;
		return false;
	}

	private bool TryGetInterfaceTypeValue(Type propertyType, [NotNullWhen(true)] out TSchema? returnType)
	{
		var interfaces = propertyType.GetInterfaces();
		var interfaceType = interfaces.FirstOrDefault(ContainsKey);
		if (interfaceType is null)
		{
			returnType = null;
			return false;
		}

		returnType = this[interfaceType];
		return true;
	}

	private bool TryGetNonNullableTypeValue(Type propertyType, [NotNullWhen(true)] out TSchema? returnType)
	{
		if (propertyType.ToContextualType().IsNullableType)
		{
			var underlying = propertyType.GetGenericArguments()[0];

			if (TryGetValue(underlying, out var schema))
			{
				returnType = schema;
				return true;
			}
		}

		returnType = null;
		return false;
	}
}
