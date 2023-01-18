using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Oxx.Backend.Generators.TypedValues.Generator;

[Generator]
public class TypedValuesGenerator : IIncrementalGenerator
{
	private const string FileSuffix = ".g.cs";
	
	private static string GetFileName(string name) => name + FileSuffix;
	public void Initialize(IncrementalGeneratorInitializationContext context)
	{
		CreateInterfaces(context);
		ImplementPartials(context);
	}

	private static void CreateInterfaces(IncrementalGeneratorInitializationContext context)
	{
		context.RegisterPostInitializationOutput(ctx
			=> ctx.AddSource(GetFileName(nameof(Interfaces.TypedValue)), SourceText.From(Interfaces.TypedValue, Encoding.UTF8)));

		context.RegisterPostInitializationOutput(ctx 
			=> ctx.AddSource(GetFileName(nameof(Interfaces.TypedId)), SourceText.From(Interfaces.TypedId, Encoding.UTF8)));
	}
	
	private static void ImplementPartials(IncrementalGeneratorInitializationContext context)
	{
		var typesImplementingTypedValue = context.SyntaxProvider
			.CreateSyntaxProvider(predicate: static (s, _) => IsSyntaxTargetForGenration(s),
				transform: static (ctx, _) => GetSemanticTargetForGeneration(ctx))
			.Where(static m => m is not null);
	}
}