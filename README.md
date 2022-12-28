# Source Generators

This repository contains a set of source generators for C#.

## Poco Schema Generator

Converts C# classes and structs into various Schemas.

In this section I will refer to these as POCOs (Plain Old CLR/C# Objects).

## [Zod Poco Schema Generator](src/Generators/PocoSchemaGenerator/Oxx.Backend.Generators.PocoSchema.Zod)

The reason I chose to generate Zod schemas instead of Typescript is because Zod is a runtime schema validator - meaning that it can be used to validate the data at runtime, unlike Typescript which is only used for type checking at compile time. 

Zod also has a Typescript type function - `z.infer<>` - which can be used to generate the Typescript types from the Zod schemas.

[Zod Website](https://zod.dev/)

[Zod Github](https://github.com/colinhacks/zod)

## Contributing

Contributions are welcome, feel free to open a pull request.
## Authors

* **Kenneth Hoff** - *Everything*
