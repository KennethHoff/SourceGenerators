using System.Reflection;
using Namotion.Reflection;
using Oxx.Backend.Generators.PocoSchema.Core.Attributes;

namespace Oxx.Backend.Generators.PocoSchema.Core.Models.Types;

public readonly struct SchemaMemberInfo
{
	private readonly MemberInfo _memberInfo;

	public SchemaMemberInfo(MemberInfo memberInfo)
	{
		EnsurePropertyOrField(memberInfo);

		_memberInfo = memberInfo;
	}
	
	private static void EnsurePropertyOrField(MemberInfo memberInfo)
	{
		if (memberInfo is not PropertyInfo and not FieldInfo)
		{
			throw new ArgumentException("MemberInfo must be a property or field.", nameof(memberInfo));
		}
	}
	
	public Type MemberType => _memberInfo switch
	{
		PropertyInfo propertyInfo => propertyInfo.PropertyType,
		FieldInfo fieldInfo => fieldInfo.FieldType,
		_ => throw new InvalidOperationException("MemberInfo must be a property or field.")
	};

	public string MemberName => _memberInfo.Name;

	public ContextualAccessorInfo ContextualType => _memberInfo.ToContextualAccessor();

	public bool IsIgnored => _memberInfo.GetCustomAttribute<SchemaIgnoreAttribute>() is not null;
}

