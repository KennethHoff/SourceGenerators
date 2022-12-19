namespace Oxx.Backend.Generators.PocoSchema.Core.Configuration;

public interface ISchemaGeneratorConfigurationBuilder<TConfigurationType> where TConfigurationType: ISchemaGeneratorConfiguration
{
	TConfigurationType Build();
}