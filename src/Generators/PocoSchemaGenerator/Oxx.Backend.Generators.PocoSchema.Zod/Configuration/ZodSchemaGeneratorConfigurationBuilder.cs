using Oxx.Backend.Generators.PocoSchema.Core.Configuration.Abstractions;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.BuiltIn;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Custom;

namespace Oxx.Backend.Generators.PocoSchema.Zod.Configuration;

public class ZodSchemaGeneratorConfigurationBuilder : SchemaGeneratorConfigurationBuilder<IZodSchemaType, ZodSchemaGeneratorConfiguration>
{
	public ZodSchemaGeneratorConfigurationBuilder()
	{
		Substitute<int, NumberZodSchemaType>();
		Substitute<float, NumberZodSchemaType>();
		Substitute<double, NumberZodSchemaType>();
		Substitute<decimal, NumberZodSchemaType>();
		Substitute<string, StringZodSchemaType>();
		Substitute<Guid, GuidZodSchemaType>();
	}
	public override void Substitute<TPoco, TSubstitute>()
	{
		var pocoType = typeof(TPoco);
		if (!SchemaTypeDictionary.ContainsKey(pocoType))
		{
			SchemaTypeDictionary.Add(pocoType, new TSubstitute());
		}
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
}