using System.Diagnostics.CodeAnalysis;

namespace Oxx.Backend.Generators.PocoSchema.Core.Models.Types;

public sealed class TypeTypeDictionary : Dictionary<Type, Type>
{
	public Type GetRelatedType(Type type)
	{
		if (TryGetValue(type, out var relatedType))
		{
			return relatedType;
		}

		if (TryGetRelatedBaseType(type, out relatedType))
		{
			return relatedType;
		}

		if (TryGetRelatedInterface(type, out relatedType))
		{
			return relatedType;
		}

		throw new InvalidOperationException($"Type {type} is not registered in the dictionary.");
	}

	public bool HasRelatedType(Type type)
		=> type switch
		{
			_ when ContainsKey(type)         => true,
			_ when HasRelatedBaseType(type)  => true,
			_ when HasRelatedInterface(type) => true,
			_                                => false,
		};

	private bool HasRelatedBaseType(Type type)
		=> TryGetRelatedBaseType(type, out _);

	private bool HasRelatedInterface(Type type)
		=> TryGetRelatedInterface(type, out _);


	private bool TryGetRelatedBaseType(Type type, [NotNullWhen(true)] out Type? relatedType)
	{
		var baseType = type.BaseType;
		if (baseType == null)
		{
			relatedType = null;
			return false;
		}

		if (TryGetValue(baseType, out relatedType))
		{
			return true;
		}

		var hasRelatedBaseType = TryGetRelatedBaseType(baseType, out relatedType);
		return hasRelatedBaseType;
	}

	private bool TryGetRelatedInterface(Type type, [NotNullWhen(true)] out Type? relatedType)
	{
		var interfaces = type.GetInterfaces();

		if (interfaces.Length == 0)
		{
			relatedType = null;
			return false;
		}

		foreach (var (key, value) in this)
		{
			foreach (var interfaceType in interfaces)
			{
				if (!interfaceType.IsGenericType || !key.IsGenericType)
				{
					continue;
				}

				var genericInterfaceType = interfaceType.GetGenericTypeDefinition();
				var genericKeyType = key.GetGenericTypeDefinition();
				if (genericInterfaceType == genericKeyType)
				{
					relatedType = value;
					return true;
				}
			}
		}

		relatedType = null;
		return false;
	}
}
