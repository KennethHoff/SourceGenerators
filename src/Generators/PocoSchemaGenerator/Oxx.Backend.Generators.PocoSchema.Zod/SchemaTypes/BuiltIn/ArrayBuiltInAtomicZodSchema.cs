using Namotion.Reflection;
using Oxx.Backend.Generators.PocoSchema.Core.Models.Types;
using Oxx.Backend.Generators.PocoSchema.Zod.Configuration;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts.Models;

namespace Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.BuiltIn;

public class ArrayBuiltInAtomicZodSchema<TUnderlyingSchema> : IGenericZodSchema, IAdditionalImportZodSchema, IBuiltInAtomicZodSchema
	where TUnderlyingSchema : IPartialZodSchema, new()
{
	public IEnumerable<ZodImport> AdditionalImports => new[]
	{
		Configuration.CreateStandardImport(UnderlyingSchema),
	};

	public ZodSchemaConfiguration Configuration
	{
		get => _configuration ?? throw new InvalidOperationException("Configuration is null");
		set => _configuration = value;
	}

	public SchemaMemberInfo MemberInfo
	{
		get => _memberInfo ?? throw new InvalidOperationException("PropertyInfo is null");
		set => _memberInfo = value;
	}

	public SchemaDefinition SchemaDefinition
	{
		get
		{
			var schemaName = Configuration.FormatSchemaName(UnderlyingSchema);

			if (ListElement.Nullability is Nullability.Nullable)
			{
				schemaName += ".nullable()";
			}

			return new SchemaDefinition($"z.array({schemaName})");
		}
	}

	private ZodSchemaConfiguration? _configuration;
	private SchemaMemberInfo? _memberInfo;


	private ContextualType ListElement => MemberInfo.MemberType switch
	{
		// Edge case for Arrays using the funky [] syntax
		{ IsArray: true } arrayType => arrayType.ToContextualType().ElementType!,
		{ } enumerableType          => enumerableType.GetGenericArguments().Single().ToContextualType(),
	};

	private IPartialZodSchema UnderlyingSchema => Configuration.CreatedSchemasDictionary.GetSchemaForType(ListElement)
											   ?? throw new InvalidOperationException($"Could not find schema for type {ListElement}");
}
