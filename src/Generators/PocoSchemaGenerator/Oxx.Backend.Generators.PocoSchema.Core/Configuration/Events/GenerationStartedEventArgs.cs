namespace Oxx.Backend.Generators.PocoSchema.Core.Configuration.Events;

public sealed class GenerationStartedEventArgs : EventArgs
{
	public GenerationStartedEventArgs(DateTime generationStartedTime)
	{
		GenerationStartedTime = generationStartedTime;
	}

	public DateTime GenerationStartedTime { get;  } 
}