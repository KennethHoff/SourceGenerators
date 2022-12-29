using Oxx.Backend.Generators.PocoSchema.Core.Models.Schemas.Contracts;

namespace Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;

public interface IMolecularZodSchema : IMolecularSchema<IPartialZodSchema>, IAdditionalImportZodSchema
{ }
