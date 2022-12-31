using System.Diagnostics;
using Oxx.Backend.Generators.PocoSchema.Core.Configuration;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.BuiltIn.Contracts;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts.Models;

namespace Oxx.Backend.Generators.PocoSchema.Zod.Configuration;

public class ZodDirectoryOutputConfiguration : IDirectoryOutputConfiguration
{
	public string Root { get; set; } = string.Empty;
	public string Atoms { get; set; } = string.Empty;
	public string Enums { get; set; } = string.Empty;
	public string Molecules { get; set; } = string.Empty;
	public TypeScriptConfiguration TypeScript { get; set; } = new();

	public DirectoryInfo AtomsDirectoryInfo => new(GetPathFromRoot(Atoms));
	public DirectoryInfo EnumsDirectoryInfo => new(GetPathFromRoot(Enums));
	public DirectoryInfo MoleculesDirectoryInfo => new(GetPathFromRoot(Molecules));
	public DirectoryInfo RootDirectoryInfo => new(Root);

	private string GetPathFromRoot(string subPath)
	{
		var combine = Path.Combine(Root, subPath);
		return combine;
	}

	public bool Valid => string.IsNullOrWhiteSpace(Root) is false
					  && string.IsNullOrWhiteSpace(Atoms) is false
					  && string.IsNullOrWhiteSpace(Enums) is false
					  && string.IsNullOrWhiteSpace(Molecules) is false
					  && TypeScript.Valid;

	private string GetAliasRoot(IPartialZodSchema schemaToImport)
		=> schemaToImport switch
		{
			IEnumZodSchema             => TypeScript.Alias + Enums,
			IAtomicZodSchema           => TypeScript.Alias + Atoms,
			IPartialMolecularZodSchema => TypeScript.Alias + Molecules,
			_                          => throw new UnreachableException("What kind of schema is this?"),
		};
	
	public ZodImport CreateImport(IPartialZodSchema schemaToImport, string schemaName)
		=> new()
		{
			FilePath = GetAliasRoot(schemaToImport) + schemaName,
			SchemaName = schemaName,
		};
}
