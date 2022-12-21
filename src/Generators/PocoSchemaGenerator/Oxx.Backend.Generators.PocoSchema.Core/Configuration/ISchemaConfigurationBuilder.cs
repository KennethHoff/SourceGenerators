namespace Oxx.Backend.Generators.PocoSchema.Core.Configuration;

public interface ISchemaConfigurationBuilder<TConfigurationType> where TConfigurationType: ISchemaConfiguration
{
	TConfigurationType Build();
}