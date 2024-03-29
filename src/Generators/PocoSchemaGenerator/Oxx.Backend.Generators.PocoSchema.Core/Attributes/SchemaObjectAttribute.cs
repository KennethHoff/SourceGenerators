﻿using Oxx.Backend.Generators.PocoSchema.Core.Models.Pocos;

namespace Oxx.Backend.Generators.PocoSchema.Core.Attributes;

/// <summary>
///     This attribute is used to mark a class or struct as a POCO (Plain Old CLR/C# Object). <br />
///     A POCO is a data structure that has no logic in it, and only contains methods to get and set its internal state. <br />
///     This attribute is used to generate a schema for the class or struct.
/// </summary>
/// <remarks>
///     The schema generator only exports properties, not fields, and includes all properties regardless of their access modifiers, unless they are marked with the
///     <see cref="SchemaMemberIgnoreAttribute" /> attribute.
/// </remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, Inherited = false)]
public class SchemaObjectAttribute : SchemaTypeAttribute<PocoObject>
{ }
