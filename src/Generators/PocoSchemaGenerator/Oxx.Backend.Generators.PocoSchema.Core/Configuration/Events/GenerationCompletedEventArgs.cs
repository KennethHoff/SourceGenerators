namespace Oxx.Backend.Generators.PocoSchema.Core.Configuration.Events;

public sealed class GenerationCompletedEventArgs : EventArgs
{
	public DateTime GenerationCompletedTime { get; }
	public DateTime GenerationStartedTime { get; } 

	public GenerationCompletedEventArgs(DateTime generationCompletedTime, DateTime generationStartedTime)
	{
		GenerationCompletedTime = generationCompletedTime;
		GenerationStartedTime = generationStartedTime;
	}
}