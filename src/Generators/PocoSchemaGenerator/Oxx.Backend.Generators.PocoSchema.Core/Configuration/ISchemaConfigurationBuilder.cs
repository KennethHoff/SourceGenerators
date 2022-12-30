using System.Runtime.CompilerServices;
using Oxx.Backend.Generators.PocoSchema.Core.Configuration.Abstractions;
using Oxx.Backend.Generators.PocoSchema.Core.Configuration.Events;
using Oxx.Backend.Generators.PocoSchema.Core.Models.Schemas.Contracts;


namespace Oxx.Backend.Generators.PocoSchema.Core.Configuration;

public interface ISchemaConfigurationBuilder<out TSelf, in TSchemaType, out TConfigurationType, out TSchemaEventConfiguration> where TSchemaType : class, ISchema
	where TSelf : ISchemaConfigurationBuilder<TSelf, TSchemaType, TConfigurationType, TSchemaEventConfiguration>
	where TConfigurationType : ISchemaConfiguration<TSchemaType, TSchemaEventConfiguration>
	where TSchemaEventConfiguration : ISchemaEventConfiguration, new()
{
	TSelf ApplyAtomicSchema<TType, TSchema>(Func<TSchema>? schemaFactory = null)
		where TSchema : TSchemaType, IAtomicSchema, new();
	TSelf ApplyGenericSchema(Type genericType, Type genericSchema);
	TConfigurationType Build();
	TSelf ConfigureEvents(Action<TSchemaEventConfiguration> action);
	TSelf OverrideFileDeletionMode(FileDeletionMode fileFileDeletionMode);
	TSelf OverrideFileExtensionInfix(string infix);
	TSelf OverrideFileNameNamingFormat(string format);
	TSelf OverrideSchemaNamingFormat(string format);
	TSelf OverrideSchemaTypeNamingFormat(string format);
	TSelf OverrideSchemaEnumNamingFormat(string format);
	TSelf ResolveTypesFromAssemblyContaining<TType>();

	/// <param name="rootDirectory">Either absolute path, or a path relative to the file where this method is called.</param>
	TSelf SetRootDirectory(string rootDirectory, [CallerFilePath] string callerFilePath = "");
}
