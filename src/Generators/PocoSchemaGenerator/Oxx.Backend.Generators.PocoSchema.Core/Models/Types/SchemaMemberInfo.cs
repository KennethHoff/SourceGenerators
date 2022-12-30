using System.Diagnostics;
using System.Reflection;
using Namotion.Reflection;
using Oxx.Backend.Generators.PocoSchema.Core.Attributes;

namespace Oxx.Backend.Generators.PocoSchema.Core.Models.Types;

[DebuggerDisplay("{Name} ({Type})")]
public readonly struct SchemaMemberInfo
{
	public Type Type => _memberInfo switch
	{
		PropertyInfo propertyInfo => propertyInfo.PropertyType,
		FieldInfo fieldInfo       => fieldInfo.FieldType,
		_                         => throw new UnreachableException(),
	};

	public string Name => _memberInfo.Name;
	
	public ContextualAccessorInfo ContextualType => _memberInfo.ToContextualAccessor();

	public bool IsIgnored => _memberInfo.GetCustomAttribute<SchemaMemberIgnoreAttribute>() is not null;
	private readonly MemberInfo _memberInfo;

	private static void EnsurePropertyOrField(MemberInfo memberInfo)
	{
		if (memberInfo is not PropertyInfo and not FieldInfo)
		{
			throw new ArgumentException("MemberInfo must be a property or field.", nameof(memberInfo));
		}
	}

	public SchemaMemberInfo(MemberInfo memberInfo)
	{
		EnsurePropertyOrField(memberInfo);

		_memberInfo = memberInfo;
	}
}
