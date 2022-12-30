using System.Runtime.CompilerServices;
using Oxx.Backend.Generators.PocoSchema.Core.Configuration.Abstractions;
using Oxx.Backend.Generators.PocoSchema.Core.Configuration.Events;
using Oxx.Backend.Generators.PocoSchema.Core.Models.Schemas.Contracts;


namespace Oxx.Backend.Generators.PocoSchema.Core.Configuration;

public interface ISchemaConfigurationBuilder<out TSelf, in TSchema, out TSchemaConfiguration, out TSchemaEvents> where TSchema : class, ISchema
	where TSelf : ISchemaConfigurationBuilder<TSelf, TSchema, TSchemaConfiguration, TSchemaEvents>
	where TSchemaConfiguration : ISchemaConfiguration<TSchemaEvents>
	where TSchemaEvents : ISchemaEvents, new()
{
	TSelf ApplyAtomicSchema<TType, TAtomicSchema>(Func<TAtomicSchema>? schemaFactory = null)
		where TAtomicSchema : TSchema, IAtomicSchema, new();
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

	/// <param name="rootDirectory">Either absolute path, or a path relative to the file where this method is called.</param>
	/// <param name="callerFilePath">Automatically passed by the compiler.</param>
	TSelf SetRootDirectory(string rootDirectory, [CallerFilePath] string callerFilePath = "");
}
