using Oxx.Backend.Generators.PocoSchema.Core.Attributes;

namespace TestingApp.Models;

[SchemaObject]
internal readonly record struct Name(string Firstname, string? MiddleName, string Lastname);