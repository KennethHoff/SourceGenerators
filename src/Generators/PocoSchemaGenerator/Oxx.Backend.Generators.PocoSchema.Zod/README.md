<h1 align="center">Zod Poco Schema Generator</h1>
<p align="center">Streamline your development process with our comprehensive C#-to-TypeScript schema mapping library, which includes automatic schema generation and runtime type-safety.</p>

## Usage

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

### Configuration

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

* `ApplySchema<TType, TSchema>` - Applies a schema to a type.
    * Example: `ApplySchema<MyClass, MySchema>()` will apply the schema `MySchema` to the type `MyClass`.
    * This is useful in the following scenarios:
        * You want to apply a schema to a type you don't own
        * It is not possible to add the `[PocoObject]` attribute to the type
        * You want to apply the schema to a type that is not a class or struct
        * You want to apply a different schema to a type than the default one
        * You want to apply a different schema to nullable and non-nullable types
            * Example: `ApplySchema<int?, MyNullableIntSchema>()` will apply the schema `MyNullableIntSchema` to the type `int?`, but keeps the default schema
              for `int`.
    * Note: This will override any previously applied schemas.
    * Note: This will also apply to all derived types of the type you specified (Unless otherwise overridden - the more specific type will be used).
    * Note: This will also apply to all types that implement the type you specified (Unless otherwise overridden - the more specific type will be used).
    * Note: This will apply the schema to both nullable and non-nullable types.
        * This only applies to Value Types (int, double, etc.) and not Reference Types. You can currently not have a separate schema for a nullable and
          non-nullable reference type, so reference types will always use the same schema (It will however have .nullable() applied to it if it is nullable - as
          will ValueTypes)
        * Note: If you want to apply a schema to a nullable type, but not to a non-nullable type, you can use the `ApplySchema<TType?, TSchema>` method.
        * Note: If you want to apply a schema to a non-nullable type, but not to a nullable type, you have to first apply the schema to both (using
          the `ApplySchema<TType, TSchema>` method), and then override the schema for the nullable type (using the `ApplySchema<TType?, TSchema>` method).

* `ApplyGenericSchema(Type, Type)` - Applies a generic schema to a generic type.
    * Example: `ApplyGenericSchema(typeof(MyClass<>), typeof(MySchema<>))` will apply the schema `MySchema<T>` to the type `MyClass<>`.
    * This is useful if you want to apply a schema to a generic type.
    * Note: The number of generic parameters must match.
    * Note: The number of generic arguments must match.

* `ConfigureEvents(Action<TSchemaEventConfiguration> action)` - Configures the events that are fired during the generation process.
    * Example: `ConfigureEvents(x => x.FileCreated += (sender, args) => { /* Do something */ })`

* `ResolveTypesFromAssemblyContaining<TType>()` - Resolves types from the assembly containing the specified type.
    * It's recommended to use a "Assembly Marker" interface like `I<ProjectName>AssemblyMarker` to resolve the assembly.
    * Example: `ResolveTypesFromAssemblyContaining<IProjectNameAssemblyMarker>()` will resolve all types from the assembly containing
      the `IProjectNameAssemblyMarker` interface.

## Known issues

In order to do these issues, I reckon a few full rewrites of the code generator would be required.
Currently the generator lacks a lot of context. There are a few issues with the current context:

1. It doesn't know of the full context of the schema it's generating, each property is mapped individually, so it doesn't know if the "surrounding" class is
   generic, or if it's a class or struct, etc.

### Very high priority

Issues that are currently blocking the release of the package.

### High priority

Issues that are common and annoying, but not blocking the release of the package.

* The schema generator stops if you have a collection of a type that doesn't have a schema.
    * Example: `public List<MyClass> MyClasses { get; set; }` will stop the schema generator if `MyClass` doesn't have a schema.

### Medium priority

Issues that are less common and/or can be worked around.

* The schema generator only partially support enums.
    * Example: `enum MyEnum { MyValue }`
    * The schema generator has a `SimpleEnumBuiltInAtomicZodSchema` schema, but it is very limited.
        * It does not generate a custom schema&type for each enum, but rather spews out `z.enum(<values>)` everywhere it's used.
            * This means that you can't access the type of the enum
* The schema generator only partially supports inheritance.
    * Example: `class MyParentClass { string MyProperty { get; set; } } class MyChildClass : MyParentClass { }`
    * Currently, the schema generator has no way of knowing that `MyChildClass` inherits from `MyParentClass`, so it will generate two separate schemas for
      them.
* The schema generator only partially supports interfaces
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
        * However, the `MyOtherClass` will have TypeScript errors as it will then have a reference to itself (`MyOtherClass.MyProperty` will be of
          type `MyOtherClass`).
* The schema generator doesn't support generic types.
    * Example: `class MyClass<T> { public T MyProperty { get; set; } }`
    * I'm sure it's possible to do this, but I imagine it would be very complicated.
    * Although, it might be easier to do this than self-referencing types as this is entirely in the scope of the schema generator.

I'm sure there are tons of other issues, but these are the ones I'm aware of.

## Future plans

* Add tests - and a lot of them.
* Add Roslyn analyzers to ensure that the classes are valid for the schema generator.
    * This will be a separate - recommended - package.
        * There might even be a separate package for each Schema, as there might be some issues that are specific to each schema.
    * This will include things like:
        * Ensuring that the `[PocoObject]` attribute is not applied to Static classes.
        * Ensuring that the `[PocoObject]` attribute is not applied to classes that does not have a public parameterless constructor.
        * Telling you if you're using a type that is not supported by the schema generator.
            * If the intention is to not export the type, then you should use the `[PocoPropertyIgnore]` attribute on the property.
            * This might not be possible to do however.
* Add more schema generators.
    * TypeScript
        * For when you don't want to use Zod - which will be the recommended schema generator, as it has runtime validation.
    * OpenAPI
        * For when all you want is a schema for your API.
        * (Probably won't do this: There are already plenty of packages that do this, and I don't think it's worth it to create another one.)
