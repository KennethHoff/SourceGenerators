namespace Oxx.Backend.Generators.PocoSchema.Core.Configuration.Events;

public sealed class GenerationCompletedEventArgs : EventArgs
{
	public DateTime GenerationStartedTime { get; } 
	public DateTime GenerationCompletedTime { get; }

	public GenerationCompletedEventArgs(DateTime generationStartedTime, DateTime generationCompletedTime)
	{
		GenerationStartedTime = generationStartedTime;
		GenerationCompletedTime = generationCompletedTime;
	}
}