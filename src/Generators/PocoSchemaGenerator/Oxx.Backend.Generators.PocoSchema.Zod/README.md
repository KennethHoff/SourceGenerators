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
    * Note: This will first check if all the files follow the naming convention of the generator. If they don't, the generator will immediately exit.

* `OverrideSchemaNamingFormat(string)` - Overrides the naming format for the schemas.
    * Default: `{0}Schema` where `{0}` is the name of the class.
    * Example: `OverrideSchemaNamingFormat("{0}Schema")` will generate a schema called `myClassSchema` for a POCO called `MyClass`.

* `OverrideSchemaTypeNamingFormat(string)` - Overrides the naming format for the types of the schemas.
    * Default: `{0}SchemaType` where `{0}` is the name of the class.
    * Example: `OverrideSchemaTypeNamingFormat("{0}SchemaType")` will generate a type called `myClassSchemaType` for a POCO called `MyClass`.

* `OverrideFileNameNamingFormat(string)` - Overrides the naming format for the file names.
    * Default: `{0}Schema` where `{0}` is the name of the class.
    * Example: `OverrideFileNameNamingFormat("{0}Schema")` will generate a file called `myClassSchema.g.ts` for a POCO called `MyClass`.
        * Note: The file extension infix(`.g`) can be changed by using the `OverrideFileExtensionInfix(string)` method (see below).

* `OverrideSchemaEnumNamingFormat(string)` - Overrides the naming format for the enums.
    * Default: `{0}SchemaEnum` where `{0}` is the name of the enum.
    * Example: `OverrideSchemaEnumNamingFormat("{0}SchemaEnum")` will generate an enum called `myClassSchemaEnum` for an enum called `MyClass`.

* `OverrideFileExtensionInfix(string)` - Overrides the infix for the file extension
  * Default: `.g`
  * Example: `OverrideFileExtensionInfix(".g")` will generate a file called `myClassSchema.g.ts` for a POCO called `MyClass`.

* `ApplyAtomicSchema<TType, TSchema>` - Applies a schema to a type.
    * Example: `ApplyAtomicSchema<MyClass, MySchema>()` will apply the schema `MySchema` to the type `MyClass`.
    * This is useful in the following scenarios:
        * You want to apply a schema to a type you don't own
        * It is not possible to add the `[SchemaObject]` attribute to the type
        * You want to apply the schema to a type that is not a class or struct
        * You want to apply a different schema to a type than the default one
        * You want to apply a different schema to nullable and non-nullable types
            * Example: `ApplyAtomicSchema<int?, MyNullableIntSchema>()` will apply the schema `MyNullableIntSchema` to the type `int?`, but keeps the default
              schema
              for `int`.
    * Note: This will override any previously applied schemas.
    * Note: This will also apply to all derived types of the type you specified (Unless otherwise overridden - the more specific type will be used).
    * Note: This will also apply to all types that implement the type you specified (Unless otherwise overridden - the more specific type will be used).
    * Note: This will apply the schema to both nullable and non-nullable types.
        * This only applies to Value Types (int, double, etc.) and not Reference Types. You can currently not have a separate schema for a nullable and
          non-nullable reference type, so reference types will always use the same schema (It will however have .nullable() applied to it if it is nullable - as
          will ValueTypes)
        * Note: If you want to apply a schema to a nullable type, but not to a non-nullable type, you can use the `ApplyAtomicSchema<TType?, TSchema>` method.
        * Note: If you want to apply a schema to a non-nullable type, but not to a nullable type, you have to first apply the schema to both (using
          the `ApplyAtomicSchema<TType, TSchema>` method), and then override the schema for the nullable type (using the `ApplyAtomicSchema<TType?, TSchema>`
          method).

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
Currently the generator lacks a lot of context. There are a few issues with the current implementation:

1. The schema doesn't know of the full context of the schema it's generating, each member is mapped individually, so it doesn't know if the "surrounding"
   class is
   generic, or if it's a class or struct, etc.
2. Too much logic is in the code generator, and not in the schema. This makes it hard to add new features, and makes it hard to override the default behavior.
    * This made it awkward to implement enums, as they work differently than other types (Requiring a separate "enum" type in addition to the schema itself and
      its type)
3. Naming needs improvement. The word "Schema" is used a lot, and it's not particularly clear what it means. Additionally, "Atoms", "Molecules" is used in the
   code, but it's not really clear what they are, and they're not really used consistently.
4. I've purposely not tried to keep the code as clean as possible, as I figured it would be easier to rewrite it from scratch when I understand the full
   extent of the issues.
    * As with most things, it started clean, but as I added more and more features, it got more and more messy and by the time I got to enums, I effectively
      gave up
      on keeping it clean as I knew I would have to rewrite it anyway.
    * I'm trying to implement as many features as possible before I start rewriting it. Basically, if I get to a point where I can't implement a feature without
      rewriting the code generator, I'll start rewriting it.

### Very high priority

Issues that are currently blocking the release of the package.

### High priority

Issues that are common and annoying, but not blocking the release of the package.

### Medium priority

Issues that are less common and/or can be worked around.

* The schema generator doesn't support multiple types with the same name
  * Example:
  * ```csharp
    [SchemaObject]
    public class MyClass
    {
        public int Id { get; set; }
    }

    [SchemaObject]
    public class MyClass
    {
        public string Name { get; set; }
    }
    ```
  * The last type will overwrite the first type.

* I'm not happy with how enums were implemented. They work, but they're not very clean.
    * I'm not sure if it's possible to implement them in a cleaner way, but I'll have to look into it.

* The schema generator only partially supports inheritance.
    * Example:
    ```csharp
    [SchemaObject]
    class BaseClass
    {
       string BaseProperty { get; set; }
    }
  
    [SchemaObject]
    class DerivedClass : BaseClass
    {
       string DerivedProperty { get; set; }
    }
    ```
    * Currently, the schema generator has no way of knowing that `DerivedClass` inherits from `BaseClass`, so it will generate two separate schemas for
      them.
    * Ideally it would generate a single schema for `BaseClass` and then a separate schema for `DerivedClass` that extends the schema for `BaseClass`.
* The schema generator only partially supports interfaces
    * Example:
    ```csharp
    [SchemaObject]
    interface IMyInterface
    {
       string MyProperty { get; set; }
    }
    ```
    * Currently it will generate a schema that's identical to what it would generate for a class. That is to say, all properties will have to match the schema,
      and all other properties will be discarded.
        * It also names it `iMyInterfaceSchema`, which is not ideal.
    * Ideally it would generate a schema that matches the interface, but allows for additional properties.
        * I don't know what it should be named though. All I know is that it shouldn't start with a lower-case `i`.
        * Additionally, all derived interfaces should extend this schema, and all types that implement this interface should extend this schema.
    * .. Or maybe I should just remove the schema generation for interfaces altogether. I'm not sure if it's really useful other than for IEnumerable\<T> and
      similar interfaces.

* The schema generator doesn't support custom schemas for molecular types
    * Example:
    * ```csharp
      [SchemaObject]
      public sealed class MyClass
      {
          string Name { get; set; }
          int Age { get; set; }
      }
      public sealed class MySchema : IMolecularZodSchema
      {
          // Schema Stuff
      }

      // Program.cs

      configuration.ApplyMolecularSchema<MyClass, MySchema>()
      // The above method doesn't exist, but it should 
      // And it should probably be renamed to ApplySchema, 
      // and merged with ApplyAtomicSchema.

      // .. That used to be the case, but it never worked 🤷‍♂️
      // ..so I renamed it and gave it more constraints 
      ```

### Low priority

Issues that are very uncommon and/or can easily be worked around and/or are very difficult to fix.

* The schema generator doesn't support self-referencing types.
    * Example:
    * ```csharp
      [SchemaObject]
      class MyClass
      {
          MyClass MyProperty { get; set; }
      }
      ```
    * There is a way to do this in Zod, but it's quite complicated and I don't think it's worth it.
        * https://github.com/colinhacks/zod#recursive-types
    * Note: This is one of the issues that currently is not caught by the code generator, and will generate an invalid schema. 
* The schema generator doesn't support circular references.
    * Example:
    * ```csharp
      [SchemaObject]
      class MyClass
      {
          MyClass MyProperty { get; set; }
      }
      [SchemaObject]
      class MyOtherClass
      {
          IEnumerable<MyClass> MyOtherProperties { get; set; }
      }

      // Or

      [SchemaObject]
      class MyClass
      {
          MyOtherClass MyProperty { get; set; }
      }
      [SchemaObject]
      class MyOtherClass
      {
          MyClass MyOtherProperty { get; set; }
      }
      ```
  * Note: TypeScript will complain about this, but the schema will still be valid.


* The schema generator doesn't support generic types.
    * Example:
    * ```csharp
      [SchemaObject]
      class MyClass<T>
      {
          T MyProperty { get; set; }
      }
      ```
    * I'm sure it's possible to do this, but I imagine it would be very complicated.

I'm sure there are tons of other issues, but these are the ones I'm aware of.

## Future plans

* Add tests - and a lot of them.
* Improve customizability
    * Add more events
    * Add more configuration options
    * Add more ways to customize the schemas
        * Currently there's no way to only apply a schema to the non-nullable type without doing the "Apply all, override nullable" workaround.
        * Currently there's no way to apply a schema to a specific property without creating a new schema for the type.
        *
    * Add more ways to customize the schema generator
        * for example, add a way to specify "only generate elements with this accessibility" - public, internal, etc.
            * This would then be added to the Attribute itself, so you could do `[SchemaGenerator(BindingFlags = BindingFlags.Public | BindingFlags.Internal)]`
    * Add more ways to customize the types
    * Add more ways to customize the output
        * Everything currently gets its own file, but it would be nice to be able to customize this.
            * For example, you could have a "schema" folder, and then have a "schema/atoms" folder, and a "schema/molecules" folder, etc.
            * Or you could have a "schema" folder, and then have a "schema/atoms.ts" file, and a "schema/molecules.ts" file, etc.

* CLI tool
    * Currently, the only way to use this is to add it to your project and have it generate the schemas on startup.
    * While this works, it's not ideal. It slows down startup time, and it's not very flexible.
        * Technically, you could add it to a separate project and run that whenever you want, but that's not ideal either.
    * Ideally, it would be possible to watch for schema changes and trigger a rebuild whenever a schema changes.
        * This would be very useful for development, as you could just change a schema and then refresh the page to see the changes.

* Add Roslyn analyzers to ensure that the classes are valid for the schema generator.
    * This will include things like:
        * Ensuring that the `[SchemaObject]` attribute is not applied to Static classes.
        * Ensuring that the `[SchemaObject]` attribute is not applied to classes that does not have a public parameterless constructor.
        * Telling you if you're using a type that is not supported by the schema generator.
            * If the intention is to not export the type, then you should use the `[SchemaMemberIgnore]` attribute on the member.