using Oxx.Backend.Generators.PocoSchema.Zod.Core;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;

namespace Oxx.Backend.Generators.PocoSchema.Zod;

public sealed class ZodSchemaGeneratorConfigurationBuilder : SchemaGeneratorConfigurationBuilder<IZodSchemaType>
{
	public override void Substitute<TPoco, TSubstitute>()
	{
		var pocoType = typeof(TPoco);
		if (!SchemaTypeDictionary.ContainsKey(pocoType))
		{
			SchemaTypeDictionary.Add(pocoType, new TSubstitute());
		}
	}
}