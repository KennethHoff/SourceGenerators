using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts.Models;

namespace TestingApp.SchemaTypes;

internal sealed class ClampedNumberAtomicZodSchema : IAtomicZodSchema
{
	private readonly Range _range;

	public ClampedNumberAtomicZodSchema()
	{
		_range = ..100;
	}
	public ClampedNumberAtomicZodSchema(Range range)
	{
		_range = range;
	}
	public SchemaBaseName SchemaBaseName => new("ClampedNumber");

	public SchemaDefinition SchemaDefinition => new($"z.number().min({_range.Start}).max({_range.End})");
}