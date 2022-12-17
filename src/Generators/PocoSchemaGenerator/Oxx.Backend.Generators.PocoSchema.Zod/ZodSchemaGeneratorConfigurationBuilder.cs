using Oxx.Backend.Generators.PocoSchema.Zod.Core;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes;

namespace Oxx.Backend.Generators.PocoSchema.Zod;

public sealed class ZodSchemaGeneratorConfigurationBuilder : SchemaGeneratorConfigurationBuilder<IZodSchemaType>
{
	public override void Substitute<TPoco, TSubstitute>()
	{
		SchemaTypeDictionary.Add(typeof(TPoco), new TSubstitute());
	}
}