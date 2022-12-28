<h1 align="center">Source generators</h1>

<p align="center">This repository contains a set of source generators for C#</p>

<h2 align="center"><a href="src/Generators/PocoSchemaGenerator/Oxx.Backend.Generators.PocoSchema.Zod">Zod Poco Schema Generator</a></h2>
<p align="center"><span><a href="https://zod.dev/">Website</a></span>   •   <span><a href="https://github.com/colinhacks/zod">Github</a></span></p>

The reason I chose to generate Zod schemas instead of Typescript is because Zod is a runtime schema validator - meaning that it can be used to validate the data at runtime, unlike Typescript which is only used for type checking at compile time. 

Zod also has a Typescript type function - `z.infer<>` - which can be used to generate the Typescript types from the Zod schemas.

(`required` keyword here is only to reduce boilerplate; Otherwise I'd have to assign default values)
![image](https://user-images.githubusercontent.com/17124533/209864929-e041ddd7-2430-46cb-80d9-e7fd0537419f.png)
![image](https://user-images.githubusercontent.com/17124533/209864911-18f2f5cb-8539-47fa-b39a-4887b6c3f588.png)

## Contributing

Contributions are welcome, feel free to open a pull request.
## Authors

* **Kenneth Hoff** - *Everything*
