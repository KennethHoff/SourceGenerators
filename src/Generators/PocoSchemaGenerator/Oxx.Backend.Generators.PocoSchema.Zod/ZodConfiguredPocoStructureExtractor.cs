using Oxx.Backend.Generators.PocoSchema.Core.Configuration.Events;
using Oxx.Backend.Generators.PocoSchema.Core.Logic.PocoExtraction;
using Oxx.Backend.Generators.PocoSchema.Core.Models.Pocos;
using Oxx.Backend.Generators.PocoSchema.Core.Models.Pocos.Contracts;
using Oxx.Backend.Generators.PocoSchema.Core.Models.Types;
using Oxx.Backend.Generators.PocoSchema.Zod.Configuration;

namespace Oxx.Backend.Generators.PocoSchema.Zod;

internal sealed class ZodConfiguredPocoStructureExtractor : ConfiguredPocoStructureExtractor<ZodSchemaConfiguration, ZodSchemaEvents, ZodDirectoryOutputConfiguration>
{
	public ZodConfiguredPocoStructureExtractor(ZodSchemaConfiguration configuration)
		: base(configuration)
	{ }

	public override IReadOnlyCollection<IPocoStructure> GetAll()
	{
		var attributeTypes = Configuration.Assemblies.SelectMany(x => x.GetTypes());
		var (types, unsupportedTypes) = GetTypeSchemaDictionary(attributeTypes, false);

		AddAtomTypes(types);

		var pocoStructures = ParseStructures(types);

		Configuration.Events.PocoStructuresCreated?.Invoke(this, new PocoStructuresCreatedEventArgs(pocoStructures, unsupportedTypes.ToArray()));
		return pocoStructures;
	}

	public override IReadOnlyCollection<IPocoStructure> Get(IEnumerable<Type> requestedTypes, bool includeDependencies = true)
	{
		var (types, unsupportedTypes) = GetTypeSchemaDictionary(requestedTypes, includeDependencies);

		AddAtomTypes(types);
		var pocoStructures = ParseStructures(types);

		Configuration.Events.PocoStructuresCreated?.Invoke(this, new PocoStructuresCreatedEventArgs(pocoStructures, unsupportedTypes.ToArray()));
		return pocoStructures;
	}

	private void AddAtomTypes(TypeCollectionTypeDictionary types)
	{
		var appliedTypes = Configuration.CreatedSchemasDictionary.Select(x => x.Key);
		types.Add(typeof(PocoAtom), appliedTypes.ToList());
	}
}