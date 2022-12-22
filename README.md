# Source Generators

This repository contains a set of source generators for C#.

## Generators

### PocoSchemaGenerator

Converts C# classes and structs into various Schemas. 

In this section I will refer to these as POCOs (Plain Old CLR/C# Objects).

_(Currently only [Zod](https://zod.dev/) schemas are supported)_

#### Usage

Install the package:
```bash
dotnet add package Oxx.Backend.Generators.PocoSchema.Zod
```
Add the generator to your project.
```csharp
// Program.cs
var configuration = new ZodSchemaConfigurationBuilder()
	.SetRootDirectory(<path to the schema output directory>)
	// Various other options
	.Build();

var schema = new ZodSchemaConverter(configuration);
var generator = new ZodSchemaGenerator(schema, configuration);

await generator.CreateFilesAsync();

// Normal Program.cs code
```

#### Configuration

The configuration is done using the `ZodSchemaConfigurationBuilder` class.

```csharp
var configuration = new ZodSchemaConfigurationBuilder()
    .SetRootDirectory(<path to the schema output directory>)
    // Various other options
    .Build();
```

The following options are available:
* `SetRootDirectory(string path)` - Sets the root directory for the schema files. This is required.
* `DeleteExistingFiles(bool)` - Deletes all existing files in the root directory before generating new ones.
  * Default: `false` 
  * Note: This will delete the entire directory, so make sure you don't have any other files in there.
* `OverrideSchemaNamingFormat(string)` - Overrides the naming format for the schemas.
  * Default: `{0}Schema` where `{0}` is the name of the class.
  * Example: `OverrideSchemaNamingFormat("{0}Schema")` will generate a schema called `myClassSchema` for a POCO called `MyClass`.
* `OverrideSchemaTypeNamingFormat(string)` - Overrides the naming format for the types of the schemas.
  * Default: `{0}SchemaType` where `{0}` is the name of the class.
  * Example: `OverrideSchemaTypeNamingFormat("{0}SchemaType")` will generate a type called `myClassSchemaType` for a POCO called `MyClass`.
* `OverrideFileNameNamingFormat(string)` - Overrides the naming format for the file names.
  * Default: `{0}Schema` where `{0}` is the name of the class.
  * Example: `OverrideFileNameNamingFormat("{0}Schema")` will generate a file called `myClassSchema.ts` for a POCO called `MyClass`.
    * Note: The file extension can be changed by using the `OverrideFileExtension(string)` method (see below).
* `OverrideFileExtension(string)` - Overrides the file extension for the generated files.
  * Default: `.ts`
  * Example: `OverrideFileExtension(".ts")` will generate a file called `myClassSchema.ts` for a class called `MyClass`.
    * Note: The file name can be changed by using the `OverrideFileNameNamingFormat(string)` method (see above).
* `Substitute<TType, TSubstitute>` - Substitutes a reference
    * This has an optional parameter `Func<TSubstitute> substituteFactory` that is used to override the default factory for the substitute type.
* `SubstituteIncludingNullable<TType, TSubstitute>` - Substitutes a POCO type with a IZodSchema.
    * This has an optional parameter `Func<TSubstitute> substituteFactory` that is used to override the default factory for the substitute type.
* `SubstituteExcludingNullable<TType, TSubstitute>` - Substitutes a POCO type with a IZodSchema.
    * This has an optional parameter `Func<TSubstitute> substituteFactory` that is used to override the default factory for the substitute type.
* `ConfigureEvents(Action<TSchemaEventConfiguration> action)` - Configures the events that are fired during the generation process.
    * Example: `ConfigureEvents(x => x.FileCreated += (sender, args) => { /* Do something */ })`
* `ResolveTypesFromAssemblyContaining<TType>()` - Resolves types from the assembly containing the specified type.
    * It's recommended to use a "Assembly Marker" interface like `I<ProjectName>AssemblyMarker` to resolve the assembly.
    * Example: `ResolveTypesFromAssemblyContaining<IProjectNameAssemblyMarker>()` will resolve all types from the assembly containing the `IProjectNameAssemblyMarker` interface.
* `ResolveTypesFromAssembly(Assembly)` - Resolves types from the specified assembly.
    * Example: `ResolveTypesFromAssembly(typeof(MyClass).Assembly)` will resolve all types from the assembly containing the `MyClass` class.
* `ResolveTypesFromAssemblies(IEnumerable<Assembly>)` - Resolves types from the specified assemblies.
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
