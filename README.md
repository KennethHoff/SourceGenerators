# Source Generators

This repository contains a set of source generators for C#.

## Generators

### PocoSchemaGenerator

Converts C# classes into various Schemas

_(Currently only [Zod](https://zod.dev/) schemas are supported)_, but more might be added in the future, like a TypeScript schema (Although, I think using Zod directly in TypeScript is a better idea).

#### Usage

```csharp

// Install the package
dotnet add package Oxx.Backend.Generators.PocoSchema.Zod

// Add the generator to your project
// This is done at the top of the program.cs file
var configuration = new ZodSchemaConfigurationBuilder()
	.SetRootDirectory(<path to your project>)
	// Various other options
	.Build();

var schema = new ZodSchemaConverter(configuration);
var generator = new ZodSchemaGenerator(schema, configuration);

await generator.CreateFilesAsync();

// Utilize the generated files in your Frontend project
// .. It's a TypeScript file; use it.
```

#### Configuration

The configuration is done using the `ZodSchemaConfigurationBuilder` class.

```csharp
var configuration = new ZodSchemaConfigurationBuilder()
    .SetRootDirectory(<path to your project>)
    // Various other options
    .Build();
```

The following options are available:
* `DeleteExistingFiles(bool)`
  * If set to true, the generator will delete all existing files in the output directory before generating new ones.
    * Default: `false`
    * Note: This will delete the entire directory, so make sure you don't have any other files in there.
* `OverrideSchemaNamingFormat(string)`
  * This is used to override the default naming format for the generated schema files.
  * Default: `{0}Schema` where `{0}` is the name of the class.
  * Example: `OverrideSchemaNamingFormat("{0}Schema")` will generate a schema called `MyClassSchema` for a class called `MyClass`.
* `OverrideSchemaTypeNamingFormat(string)`
  * This is used to override the default naming format for the generated schema types.
  * Default: `{0}SchemaType` where `{0}` is the name of the class.
  * Example: `OverrideSchemaTypeNamingFormat("{0}SchemaType")` will generate a schema type called `MyClassSchemaType` for a class called `MyClass`.
* `OverrideFileNameNamingFormat(string)`
  * This is used to override the default naming format for the generated file names.
  * Default: `{0}Schema` where `{0}` is the name of the class.
  * Example: `OverrideFileNameNamingFormat("{0}Schema")` will generate a file called `MyClassSchema.ts` for a class called `MyClass`.
    * Note: The file extension can be changed by using the `OverrideFileExtension(string)` method (see below).
* `OverrideFileExtension(string)`
  * This is used to override the default file extension for the generated files.
  * Default: `.ts`
  * Example: `OverrideFileExtension(".ts")` will generate a file called `MyClassSchema.ts` for a class called `MyClass`.
    * Note: The file name can be changed by using the `OverrideFileNameNamingFormat(string)` method (see above).
* `SubstituteIncludingNullable<TType, TSubstitute>`
    * This has an optional parameter `Func<TSubstitute> substituteFactory` that is used to override the default factory for the substitute type.
* `SubstituteExcludingNullable<TType, TSubstitute>`
    * This has an optional parameter `Func<TSubstitute> substituteFactory` that is used to override the default factory for the substitute type.
* `Substitute<TType, TSubstitute>`
    * This has an optional parameter `Func<TSubstitute> substituteFactory` that is used to override the default factory for the substitute type.
* `ConfigureEvents(Action<TSchemaEventConfiguration> action)`
    * This is used to configure the events that are fired during the generation process.
    * Example: `ConfigureEvents(x => x.FileCreated += (sender, args) => { /* Do something */ })`
* `ResolveTypesFromAssemblyContaining<TType>()`
  * This is used to resolve types from a specific assembly. Required in order to create schemas, as the generator needs to know what types to create schemas for.
    * It's recommended to use a "Assembly Marker" interface like `I<ProjectName>AssemblyMarker` to resolve the assembly.
    * Example: `ResolveTypesFromAssemblyContaining<IProjectNameAssemblyMarker>()` will resolve all types from the assembly containing the `IProjectNameAssemblyMarker` interface.
* `ResolveTypesFromAssembly(Assembly)`
  * This is used to resolve types from a specific assembly. Required in order to create schemas, as the generator needs to know what types to create schemas for.
  * Example: `ResolveTypesFromAssembly(typeof(MyClass).Assembly)` will resolve all types from the assembly containing the `MyClass` class.
* `ResolveTypesFromAssemblies(IEnumerable<Assembly>)`
  * This is used to resolve types from a collection of assemblies. Required in order to create schemas, as the generator needs to know what types to create schemas for.
  * Example: `ResolveTypesFromAssemblies(new[] { typeof(MyClass).Assembly, typeof(MyOtherClass).Assembly })` will resolve all types from the assemblies containing the `MyClass` and `MyOtherClass` classes.

## Future plans

* Add Roslyn analyzers to ensure that the classes are valid for the schema generator.
* Add more schema types.
  * TypeScript
  * OpenAPI
* Add more schema generators.
  * TypeScript
  * OpenAPI
## Authors

* **Kenneth Hoff** - *Everything*
