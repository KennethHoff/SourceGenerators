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
* `ApplySchemaToStruct<TType, TSchema>` - Applies a schema to a struct (Both Nullable and Non-Nullable).
  * This has an optional parameter `Func<TSubstitute> substituteFactory` that is used to override the default factory for the substitute type.
  * Example: `ApplySchemaToStruct<MyStruct, MyStructSchema>()` will apply the schema `MyStructSchema` to the struct `MyStruct`.
  * Example: `ApplySchemaToStruct<MyStruct, MyStructSchema>(() => new MyStruct())` will apply the schema `MyStructSchema` to the struct `MyStruct` and use the factory `() => new MyStruct()` to create the substitute type.
* `ApplySchemaToClass<TType, TSchema` - Applies a schema to a class.
  * This has an optional parameter `Func<TSubstitute> substituteFactory` that is used to override the default factory for the substitute type.
  * Example: `ApplySchemaToClass<MyClass, MyClassSchema>()` will apply the schema `MyClassSchema` to the class `MyClass`.
  * Example: `ApplySchemaToClass<MyClass, MyClassSchema>(() => new MyClass())` will apply the schema `MyClassSchema` to the class `MyClass` and use the factory `() => new MyClass()` to create the substitute type.
  * Note: This should very rarely be used, as you generally want to use the `[PocoObject`] attribute instead. This is only here for edge cases.
    * Example: If you don't have access to the source code of the class, but you want to apply a schema to it.
    * Internally this is only used to apply a schema to the `string` type.
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

## Known issues

### Very high priority
Issues that are currently blocking the release of the package. 
* The schema generator doesn't support generic properties.
  * Example: `class MyClass { public List<MyClass> MyProperty { get; set; } }`
* The schema generator doesn't support nullable "molecular" types.
  * A "molecular" type is a type that is created from the `[PocoObject]` attribute.
  * Example: `class MyClass { MyOtherClass? MyProperty { get; set; } }` would be identical to `class MyClass { MyOtherClass MyProperty { get; set; } }`

### Medium priority
Issues that are less common and/or can be worked around.
* The schema generator doesn't support enums.
  * Example: `enum MyEnum { MyValue }`
* The schema generator only partially supports inheritance.
  * Example: `class MyParentClass { string MyProperty { get; set; } } class MyChildClass : MyParentClass { }`
  * Currently, the schema generator has no way of knowing that `MyChildClass` inherits from `MyParentClass`, so it will generate two separate schemas for them.
* The schema generator doesn't fully support interfaces
    * Example: `interface IMyInterface { string MyProperty { get; set; } }`
    * Currently it will generate a schema that's identical to what it would generate for a class. That is to say, all properties will have to match the schema,
      and all other properties will be discarded.
    * Ideally it would generate a schema that matches the interface, but allows for additional properties.

### Low priority
Issues that are very uncommon and/or can easily be worked around and/or are very difficult to fix.
* The schema generator doesn't support self-referencing types.
    * Example: `class MyClass { public MyClass MyProperty { get; set; } }`
    * There is a way to do this in Zod, but it's quite complicated and I don't think it's worth it.
        * https://github.com/colinhacks/zod#recursive-types
    * This can partially be worked around by making an inherited class instead
      * Example: `class MyClass { public MyOtherClass MyProperty { get; set; } } class MyOtherClass : MyClass { }`
      * However, the `MyOtherClass` will have TypeScript errors as it will then have a reference to itself (`MyOtherClass.MyProperty` will be of type `MyOtherClass`).
* The schema generator doesn't support generic types.
    * Example: `class MyClass<T> { public T MyProperty { get; set; } }`
    * I'm sure it's possible to do this, but I imagine it would be very complicated.
    * Although, it might be easier to do this than self-referencing types as this is entirely in the scope of the schema generator.

I'm sure there are tons of other issues, but these are the ones I'm aware of.

## Contributing

Contributions are welcome, feel free to open a pull request.
## Authors

* **Kenneth Hoff** - *Everything*
