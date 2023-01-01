namespace Oxx.Backend.Generators.PocoSchema.Core.Configuration.Events;

public sealed class DeletingFilesEventArgs : EventArgs
{
	public Dictionary<DirectoryInfo, FileInfo[]> Dictionary { get; }

	public DeletingFilesEventArgs(Dictionary<DirectoryInfo, FileInfo[]> dictionary)
	{
		Dictionary = dictionary;
	}
}