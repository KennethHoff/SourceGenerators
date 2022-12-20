using Oxx.Backend.Generators.PocoSchema.Core.Configuration.Abstractions;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.BuiltIn;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Custom;

namespace Oxx.Backend.Generators.PocoSchema.Zod.Configuration;

public class ZodSchemaGeneratorConfigurationBuilder : SchemaGeneratorConfigurationBuilder<IZodSchemaType, ZodSchemaGeneratorConfiguration>
{
	public ZodSchemaGeneratorConfigurationBuilder()
	{
		Substitute<string, StringZodSchemaType>();
		SubstituteIncludingNullable<int, NumberZodSchemaType>();
		SubstituteIncludingNullable<float, NumberZodSchemaType>();
		SubstituteIncludingNullable<double, NumberZodSchemaType>();
		SubstituteIncludingNullable<decimal, NumberZodSchemaType>();
		SubstituteIncludingNullable<Guid, GuidZodSchemaType>();
		SubstituteIncludingNullable<bool, BooleanZodSchemaType>();
		SubstituteIncludingNullable<DateTime, DateZodSchemaType>();
		SubstituteIncludingNullable<DateOnly, DateOnlyZodSchemaType>();
	}
	public override ZodSchemaGeneratorConfigurationBuilder Substitute<TType, TSubstitute>() where TType: class
	{
		UpsertSchemaTypeDictionary<TType, TSubstitute>();
		return this;
	}

	public override ZodSchemaGeneratorConfigurationBuilder SubstituteIncludingNullable<TType, TSubstitute>() where TType: struct
	{
		UpsertSchemaTypeDictionary<TType, TSubstitute>();
		UpsertSchemaTypeDictionary<TType?, TSubstitute>();
		return this;
	}

	public override ZodSchemaGeneratorConfigurationBuilder SubstituteExcludingNullable<TType, TSubstitute>()
	{
		UpsertSchemaTypeDictionary<TType, TSubstitute>();
		return this;
	}

	protected override ZodSchemaGeneratorConfiguration CreateConfiguration()
		=> new()
		{
			SchemaTypeDictionary = SchemaTypeDictionary,
			Assemblies = Assemblies,
			OutputDirectory = OutputDirectory,
			SchemaNamingConvention = SchemaNamingConvention,
			SchemaTypeNamingConvention = SchemaTypeNamingConvention,
			DeleteFilesOnStart = DeleteFilesOnStart,
		};

	private void UpsertSchemaTypeDictionary<TType, TSubstitute>() where TSubstitute : IZodSchemaType, new()
	{
		var type = typeof(TType);
		if (SchemaTypeDictionary.ContainsKey(type))
		{
			SchemaTypeDictionary[type] = new TSubstitute();
		}
		else
		{
			SchemaTypeDictionary.Add(type, new TSubstitute());
		}
	}
}