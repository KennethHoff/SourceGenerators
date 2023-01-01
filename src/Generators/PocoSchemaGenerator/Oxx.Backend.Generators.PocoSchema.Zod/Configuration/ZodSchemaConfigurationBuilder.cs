using Oxx.Backend.Generators.PocoSchema.Core.Configuration.Abstractions;
using Oxx.Backend.Generators.PocoSchema.Core.Models.Types;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.BuiltIn;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Custom;

namespace Oxx.Backend.Generators.PocoSchema.Zod.Configuration;

public class ZodSchemaConfigurationBuilder : SchemaConfigurationBuilder<ZodSchemaConfigurationBuilder, IPartialZodSchema, IAtomicZodSchema, ZodSchemaConfiguration, ZodSchemaEvents, ZodDirectoryOutputConfiguration>
{
	protected override ZodSchemaConfiguration Configuration => new()
	{
		GenericSchemasDictionary = GenericSchemasDictionary,
		Assemblies = Assemblies,
		DirectoryOutputConfiguration = DirectoryOutputConfiguration,
		FileDeletionMode = FileDeletionMode,
		Events = EventConfiguration ?? new ZodSchemaEvents(),
		SchemaNamingFormat = SchemaNamingFormat,
		SchemaEnumNamingFormat = SchemaEnumNamingFormat,
		SchemaTypeNamingFormat = SchemaTypeNamingFormat,
		SchemaFileNameFormat = FileNameFormat,
		FileExtension = FileExtension,
		FileExtensionInfix = FileExtensionInfix,
		CreatedSchemasDictionary = CreateFromAtomDictionary(),
	};

	protected override string FileExtension { get; set; } = ".ts";
	protected override string FileExtensionInfix { get; set; } = string.Empty;
	protected override string FileNameFormat { get; set; } = "{0}Schema";
	protected override string SchemaEnumNamingFormat { get; set; } = "{0}SchemaEnum";
	protected override string SchemaNamingFormat { get; set; } = "{0}Schema";
	protected override string SchemaTypeNamingFormat { get; set; } = "{0}SchemaType";
	protected override void EnsureValidConfiguration()
	{
		if (!DirectoryOutputConfiguration.Valid)
		{
			throw new InvalidOperationException("Output directory is invalid.");
		}
		
		if (string.IsNullOrWhiteSpace(FileExtension))
		{
			throw new InvalidOperationException("File extension is invalid.");
		}
		
		if (SchemaTypeNamingFormat.Equals(SchemaNamingFormat, StringComparison.OrdinalIgnoreCase))
		{
			throw new InvalidOperationException("Schema type naming format cannot be the same as schema naming format.");
		}
		if (SchemaEnumNamingFormat.Equals(SchemaNamingFormat, StringComparison.OrdinalIgnoreCase))
		{
			throw new InvalidOperationException("Schema enum naming format cannot be the same as schema naming format.");
		}
		if (SchemaTypeNamingFormat.Equals(SchemaEnumNamingFormat, StringComparison.OrdinalIgnoreCase))
		{
			throw new InvalidOperationException("Schema type naming format cannot be the same as schema enum naming format.");
		}
	}

	public ZodSchemaConfigurationBuilder()
	{
		ApplyAtomicSchema<string, StringBuiltInAtomicZodSchema>();
		ApplyAtomicSchema<int, NumberBuiltInAtomicZodSchema>();
		ApplyAtomicSchema<float, NumberBuiltInAtomicZodSchema>();
		ApplyAtomicSchema<double, NumberBuiltInAtomicZodSchema>();
		ApplyAtomicSchema<decimal, NumberBuiltInAtomicZodSchema>();
		ApplyAtomicSchema<Guid, GuidAtomicZodSchema>();
		ApplyAtomicSchema<bool, BooleanBuiltInAtomicZodSchema>();
		ApplyAtomicSchema<DateTime, DateBuiltInAtomicZodSchema>();
		ApplyGenericSchema(typeof(IEnumerable<>), typeof(ArrayBuiltInAtomicZodSchema<>));
	}

	private TypeSchemaDictionary<IPartialZodSchema> CreateFromAtomDictionary()
	{
		var dictionary = new TypeSchemaDictionary<IPartialZodSchema>();

		foreach (var (key, value) in AtomDictionary)
		{
			dictionary.Add(key, value);
		}
		return dictionary;
	}
}