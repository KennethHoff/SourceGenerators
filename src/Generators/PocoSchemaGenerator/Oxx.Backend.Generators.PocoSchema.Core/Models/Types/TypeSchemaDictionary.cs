using System.Diagnostics.CodeAnalysis;
using Namotion.Reflection;
using Oxx.Backend.Generators.PocoSchema.Core.Models.Schemas.Contracts;

namespace Oxx.Backend.Generators.PocoSchema.Core.Models.Types;

public sealed class TypeSchemaDictionary<TSchemaType> : Dictionary<Type, TSchemaType>
	where TSchemaType : class, ISchema
{
	public TSchemaType? GetSchemaForType(Type propertyType)
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

	public TSchemaType? GetMemberForMemberInfo(SchemaMemberInfo memberInfo)
		=> GetSchemaForType(memberInfo.MemberType);

	public bool HasSchemaForType(Type propertyType)
		=> GetSchemaForType(propertyType) != null;
	
	public bool HasSchemaForMemberInfo(SchemaMemberInfo memberInfo)
		=> GetMemberForMemberInfo(memberInfo) != null;

	public void Update(Type type, TSchemaType updatedSchema)
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

	private bool TryGetNonNullableTypeValue(Type propertyType, [NotNullWhen(true)] out TSchemaType? returnType)
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
	
	private bool TryGetBaseTypeValue(Type propertyType, [NotNullWhen(true)] out TSchemaType? returnType)
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

	private bool TryGetInterfaceTypeValue(Type propertyType, [NotNullWhen(true)] out TSchemaType? returnType)
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
}
