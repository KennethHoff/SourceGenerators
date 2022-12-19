using Microsoft.CodeAnalysis;

namespace Oxx.Backend.Generators.PocoSchema.Core;

public abstract class SchemaSourceGenerator<TSchemaType> : ISourceGenerator where TSchemaType: class, ISchemaType
{
	protected abstract SchemaGeneratorConfigurationBuilder<TSchemaType> ConfigurationBuilder { get; }

	protected abstract ISchema Schema { get; }

	public void Initialize(GeneratorInitializationContext context)
	{ }

	public void Execute(GeneratorExecutionContext context)
	{
		context.AddSource("schema.ts", "export const schema = " + Schema);
		if (!ConfigurationBuilder.IsValid)
		{
			context.ReportDiagnostic(Diagnostic.Create(
				new DiagnosticDescriptor(
					"ZodSchemaGeneratorTask",
					"Zod Schema Generator Task",
					"Configuration is invalid",
					"Generator",
					DiagnosticSeverity.Error,
					true),
				Location.None));
			return;
		}

		var generator = new SchemaGenerator(Schema, ConfigurationBuilder);
		var execute = generator.CreateFiles();
		
		context.ReportDiagnostic(Diagnostic.Create(
			new DiagnosticDescriptor(
				"ZodSchemaGeneratorTask",
				"Zod Schema Generator Task",
				$"Generated {execute.Count} files",
				"Generator",
				DiagnosticSeverity.Info,
				true),
			Location.None));
	}
}

