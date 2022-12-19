using Microsoft.Build.Framework;

namespace Oxx.Backend.Generators.PocoSchema.Zod.Core;

public abstract class SchemaGeneratorTask<TSchemaType> : Microsoft.Build.Utilities.Task
	where TSchemaType: class, ISchemaType
{
	[Required]
	public string SchemaGeneratorConfigurationMethodFullNamespace { get; internal set; } = null!;

	[Required]
	public string SchemaGeneratorConfigurationMethodAssemblyName { get; internal set; } = null!;

	protected abstract SchemaGeneratorConfigurationBuilder<TSchemaType> ConfigurationBuilder { get; }

	protected abstract ISchema Schema { get; }

	public override bool Execute()
	{
		Log.LogMessage("Starting schema generation..");
		if (!ConfigurationBuilder.IsValid)
		{
			return false;
		}
		
		var generator = new SchemaGenerator(Schema, ConfigurationBuilder);
		var execute = generator.CreateFiles();
		
		Log.LogMessage("Schema generation completed.");
		return execute;
	}
}

