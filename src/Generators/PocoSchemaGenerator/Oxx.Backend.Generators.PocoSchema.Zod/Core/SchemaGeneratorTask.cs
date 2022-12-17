using Microsoft.Build.Framework;

namespace Oxx.Backend.Generators.PocoSchema.Zod.Core;

public abstract class SchemaGeneratorTask<TSchemaType> : Microsoft.Build.Utilities.Task
	where TSchemaType: class, ISchemaType
{
	[Required]
	public string SchemaGeneratorConfigurationMethodFullName { get; internal set; } = null!;

	protected abstract SchemaGeneratorConfigurationBuilder<TSchemaType> ConfigurationBuilder { get; }

	protected abstract ISchema Schema { get; }

	public override bool Execute()
	{
		if (!ConfigurationBuilder.IsValid)
		{
			return false;
		}
		
		var generator = new SchemaGenerator(Schema, ConfigurationBuilder);
		return generator.CreateFiles();
	}
}

