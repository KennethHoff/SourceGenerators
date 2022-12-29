using Oxx.Backend.Generators.PocoSchema.Core.Models.Schemas.Contracts;
using Oxx.Backend.Generators.PocoSchema.Core.Models.Types;

namespace Oxx.Backend.Generators.PocoSchema.Core.Configuration.Events.Models;

public readonly record struct CreatedSchemaInformation(Type Type, ISchema Schema, IReadOnlyCollection<SchemaMemberInfo> InvalidMembers);
