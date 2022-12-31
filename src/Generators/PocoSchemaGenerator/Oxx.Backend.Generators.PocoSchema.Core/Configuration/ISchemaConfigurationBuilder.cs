using Oxx.Backend.Generators.PocoSchema.Core.Configuration.Abstractions;
using Oxx.Backend.Generators.PocoSchema.Core.Configuration.Events;
using Oxx.Backend.Generators.PocoSchema.Core.Models.Schemas.Contracts;


namespace Oxx.Backend.Generators.PocoSchema.Core.Configuration;

public interface ISchemaConfigurationBuilder<out TSelf, in TSchema, in TAtomicSchema, out TSchemaConfiguration, out TSchemaEvents, out TDirectoryOutputConfiguration> 
	where TSelf : ISchemaConfigurationBuilder<TSelf, TSchema, TAtomicSchema, TSchemaConfiguration, TSchemaEvents, TDirectoryOutputConfiguration>
	where TSchema : class, ISchema
	where TAtomicSchema: class, TSchema, IAtomicSchema
	where TSchemaConfiguration : ISchemaConfiguration<TSchemaEvents, TDirectoryOutputConfiguration>
	where TSchemaEvents : ISchemaEvents, new()
	where TDirectoryOutputConfiguration : IDirectoryOutputConfiguration
{
	TSelf ApplyAtomicSchema<TType, TAppliedSchema>(Func<TAppliedSchema>? schemaFactory = null)
		where TAppliedSchema : TAtomicSchema, new();
	TSelf ApplyGenericSchema(Type genericType, Type genericSchema);
	TSchemaConfiguration Build();
	TSelf ConfigureEvents(Action<TSchemaEvents> action);
	TSelf OverrideFileDeletionMode(FileDeletionMode fileFileDeletionMode);
	TSelf OverrideFileExtensionInfix(string infix);
	TSelf OverrideFileNameNamingFormat(string format);
	TSelf OverrideSchemaNamingFormat(string format);
	TSelf OverrideSchemaTypeNamingFormat(string format);
	TSelf OverrideSchemaEnumNamingFormat(string format);
	TSelf ResolveTypesFromAssemblyContaining<TType>();
}
