namespace Oxx.Backend.Generators.PocoSchema.Core;

public interface ISchemaGenerator
{
	Task GenerateAllAsync();
	Task GenerateAsync<TPoco>();
	Task GenerateAsync(Type pocoType);
	Task GenerateAsync(IEnumerable<Type> pocoTypes);
}