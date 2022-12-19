namespace Oxx.Backend.Generators.PocoSchema.Zod.Core.Configuration;

public interface ISchemaGeneratorConfigurationBuilder<TConfigurationType> where TConfigurationType: ISchemaGeneratorConfiguration
{
	TConfigurationType Build();
}