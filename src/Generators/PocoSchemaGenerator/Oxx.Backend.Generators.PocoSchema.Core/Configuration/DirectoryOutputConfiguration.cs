namespace Oxx.Backend.Generators.PocoSchema.Core.Configuration;

public class DirectoryOutputConfiguration
{
	public string Atomics { get; set; } = string.Empty;
	public DirectoryInfo AtomicsDirectoryInfo => new(GetPathFromRoot(Atomics));
	public string Enums { get; set; } = string.Empty;
	public DirectoryInfo EnumsDirectoryInfo => new(GetPathFromRoot(Enums));
	public string Molecules { get; set; } = string.Empty;
	public DirectoryInfo MoleculesDirectoryInfo => new(GetPathFromRoot(Molecules));
	public string Root { get; set; } = string.Empty;

	public DirectoryInfo RootDirectoryInfo => new(Root);
	
	private string RootDirectoryFullName => Path.GetDirectoryName(RootDirectoryInfo.FullName)!;
	private bool AtomicsParentIsRoot => string.Equals(AtomicsDirectoryInfo.Parent!.FullName, RootDirectoryFullName, StringComparison.OrdinalIgnoreCase);
	private bool EnumsParentIsRoot => string.Equals(EnumsDirectoryInfo.Parent!.FullName, RootDirectoryFullName, StringComparison.OrdinalIgnoreCase);
	private bool MoleculesParentIsRoot => string.Equals(MoleculesDirectoryInfo.Parent!.FullName, RootDirectoryFullName, StringComparison.OrdinalIgnoreCase);

	public bool Valid => Configured && AtomicsParentIsRoot && EnumsParentIsRoot && MoleculesParentIsRoot;

	private bool Configured => string.IsNullOrWhiteSpace(Root) == false
							&& string.IsNullOrWhiteSpace(Atomics) == false
							&& string.IsNullOrWhiteSpace(Enums) == false
							&& string.IsNullOrWhiteSpace(Molecules) == false;


	public string GetPathFromRoot(string subPath)
	{
		var combine = Path.Combine(Root, subPath);
		return combine;
	}
	
	public string GetRelativeFromRoot(string path)
		=> Path.GetRelativePath(RootDirectoryInfo.FullName, path);

	private string GetTraversalPath(string relativeFromRoot, FileSystemInfo directoryInfo)
	{
		var traversal = Path.GetRelativePath(directoryInfo.FullName, RootDirectoryInfo.FullName);
		var traversalPath = traversal == "." ? relativeFromRoot : Path.Combine(traversal, relativeFromRoot);
		return traversalPath;
	}
	
	public string GetTraversalFromEnums(string relativeFromRoot)
		=> GetTraversalPath(relativeFromRoot, EnumsDirectoryInfo);
	public string GetTraversalFromMolecules(string relativeFromRoot)
		=> GetTraversalPath(relativeFromRoot, MoleculesDirectoryInfo);
	public string GetTraversalFromAtomics(string relativeFromRoot)
		=> GetTraversalPath(relativeFromRoot, AtomicsDirectoryInfo);
}
