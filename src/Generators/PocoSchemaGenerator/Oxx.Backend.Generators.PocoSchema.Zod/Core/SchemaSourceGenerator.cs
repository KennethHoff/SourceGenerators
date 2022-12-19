using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Oxx.Backend.Generators.PocoSchema.Zod.Core;

public abstract class SchemaSourceGenerator<TSchemaType> : ISourceGenerator where TSchemaType: class, ISchemaType
{
	protected abstract SchemaGeneratorConfigurationBuilder<TSchemaType> ConfigurationBuilder { get; }

	protected abstract ISchema Schema { get; }

	public void Initialize(GeneratorInitializationContext context)
	{ }

	public void Execute(GeneratorExecutionContext context)
	{
		var pocoObjects = FindPocoObjects(context);

		
		
		context.AddSource("schema.ts", pocoObjects.Aggregate(string.Empty, (a, b) => a + ", " + b.Name));
		// if (!ConfigurationBuilder.IsValid)
		// {
		// 	context.ReportDiagnostic(Diagnostic.Create(
		// 		new DiagnosticDescriptor(
		// 			"ZodSchemaGeneratorTask",
		// 			"Zod Schema Generator Task",
		// 			"Configuration is invalid",
		// 			"Generator",
		// 			DiagnosticSeverity.Error,
		// 			true),
		// 		Location.None));
		// 	return;
		// }
		//

		// var semanticModel = context.Compilation.GetSemanticModel(context.Compilation.SyntaxTrees.First());
		// var generator = new SchemaGenerator(Schema, ConfigurationBuilder);
		// var execute = generator.CreateFiles(semanticModel, pocoObjects);
		//
		// context.ReportDiagnostic(Diagnostic.Create(
		// 	new DiagnosticDescriptor(
		// 		"ZodSchemaGeneratorTask",
		// 		"Zod Schema Generator Task",
		// 		$"Generated {execute.Count} files",
		// 		"Generator",
		// 		DiagnosticSeverity.Info,
		// 		true),
		// 	Location.None));
	}

	private IReadOnlyCollection<PocoObject> FindPocoObjects(GeneratorExecutionContext context)
		=> context.Compilation.SyntaxTrees
			.SelectMany(tree => tree.GetRoot().DescendantNodes())
			.Where(node => node is ClassDeclarationSyntax)
			.Where(syntaxNode =>
			{
				// Check if the class has the attribute
				var classDeclaration = (ClassDeclarationSyntax)syntaxNode;
				var attributeList = classDeclaration.AttributeLists;

				return attributeList
					.SelectMany(attributeListSyntax => attributeListSyntax.Attributes)
					.Any(attributeSyntax => attributeSyntax.Name.ToString()
						.Contains("PocoObjectAttribute"));
			})
			.Select(node =>
			{
				var declaredSymbol = context.Compilation.GetSemanticModel(node.SyntaxTree)
					.GetDeclaredSymbol(node);
				if (declaredSymbol is not INamedTypeSymbol namedTypeSymbol)
				{
					return PocoObject.None;
				}

				var fileName = namedTypeSymbol.Name;
				var properties = namedTypeSymbol.GetMembers()
					.Where(member => member.Kind is SymbolKind.Property)
					.Select(member => (IPropertySymbol)member)
					.Where(member => member.DeclaredAccessibility is Accessibility.Public)
					.ToList();

				return new PocoObject(fileName, properties);
			})
			.Where(pocoObject => pocoObject != PocoObject.None)
			.ToList();
}

