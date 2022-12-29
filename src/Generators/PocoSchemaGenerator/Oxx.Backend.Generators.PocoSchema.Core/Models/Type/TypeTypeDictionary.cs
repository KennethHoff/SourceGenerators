using System.Diagnostics.CodeAnalysis;

namespace Oxx.Backend.Generators.PocoSchema.Core.Models.Type;

public sealed class TypeTypeDictionary : Dictionary<System.Type, System.Type>
{
	public System.Type GetRelatedType(System.Type type)
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

	public bool HasRelatedType(System.Type type)
		=> type switch
		{
			_ when ContainsKey(type)         => true,
			_ when HasRelatedBaseType(type)  => true,
			_ when HasRelatedInterface(type) => true,
			_                                => false,
		};

	private bool HasRelatedBaseType(System.Type type)
		=> TryGetRelatedBaseType(type, out _);

	private bool HasRelatedInterface(System.Type type)
		=> TryGetRelatedInterface(type, out _);


	private bool TryGetRelatedBaseType(System.Type type, [NotNullWhen(true)] out System.Type? relatedType)
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

	private bool TryGetRelatedInterface(System.Type type, [NotNullWhen(true)] out System.Type? relatedType)
	{
		var interfaces = type.GetInterfaces();

		if (interfaces.Length == 0)
		{
			relatedType = null;
			return false;
		}

		foreach (var interfaceType in interfaces)
		{
			foreach (var (key, value) in this)
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
