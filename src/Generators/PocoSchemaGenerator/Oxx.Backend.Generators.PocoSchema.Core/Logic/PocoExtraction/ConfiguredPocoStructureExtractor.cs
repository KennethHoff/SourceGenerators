using System.Reflection;
using Oxx.Backend.Generators.PocoSchema.Core.Attributes;
using Oxx.Backend.Generators.PocoSchema.Core.Configuration;
using Oxx.Backend.Generators.PocoSchema.Core.Configuration.Events;
using Oxx.Backend.Generators.PocoSchema.Core.Extensions;
using Oxx.Backend.Generators.PocoSchema.Core.Models.Pocos;
using Oxx.Backend.Generators.PocoSchema.Core.Models.Pocos.Contracts;
using Oxx.Backend.Generators.PocoSchema.Core.Models.Types;

namespace Oxx.Backend.Generators.PocoSchema.Core.Logic.PocoExtraction;

public class ConfiguredPocoStructureExtractor<TSchemaEvents> : IPocoStructureExtractor
	where TSchemaEvents : ISchemaEvents
{
	private readonly ISchemaConfiguration<TSchemaEvents> _configuration;

	public ConfiguredPocoStructureExtractor(ISchemaConfiguration<TSchemaEvents> configuration)
	{
		_configuration = configuration;
	}

	#region Interface implementations

	public IReadOnlyCollection<IPocoStructure> Get(IEnumerable<Type> types, bool includeDependencies = true)
	{
		var (typeSchemaDictionary, unsupportedTypes) = GetTypeSchemaDictionary(types, includeDependencies);
		
		var pocoStructures = ParseStructures(typeSchemaDictionary);

		_configuration.Events.PocoStructuresCreated?.Invoke(this, new PocoStructuresCreatedEventArgs(pocoStructures, unsupportedTypes.ToArray()));
		return pocoStructures;
	}

	public IReadOnlyCollection<IPocoStructure> GetAll()
	{
		var allTypes = _configuration.Assemblies.SelectMany(x => x.GetTypes());
		var (types, unsupportedTypes) = GetTypeSchemaDictionary(allTypes, true);
		
		var pocoStructures = ParseStructures(types);

		_configuration.Events.PocoStructuresCreated?.Invoke(this, new PocoStructuresCreatedEventArgs(pocoStructures, unsupportedTypes.ToArray()));
		return pocoStructures;
	}

	#endregion

	private void CheckSupport(Type type, bool includeDependencies, IDictionary<SchemaTypeAttribute, List<Type>> supported, ICollection<UnsupportedType> unsupported)
	{
		if (unsupported.Any(x => x.Type == type))
		{
			return;
		}
		
		var schemaTypeAttribute = type.GetCustomAttribute<SchemaTypeAttribute>();
		if (schemaTypeAttribute is null)
		{
			return;
		}

		if (!supported.ContainsKey(schemaTypeAttribute))
		{
			supported.Add(schemaTypeAttribute, new List<Type>());
		}

		if (supported[schemaTypeAttribute].Contains(type))
		{
			return;
		}

		if (includeDependencies)
		{
			foreach (var member in type.GetValidSchemaMembers())
			{
				CheckSupport(member.Type, includeDependencies, supported, unsupported);
			}
		}

		if (IsSupported(type) is { } exception)
		{
			unsupported.Add(new UnsupportedType(type, exception));
			// _configuration.Events.UnsupportedTypeFound?.Invoke(this, new UnsupportedTypeFoundEventArgs(type, exception));
			return;
		}

		supported[schemaTypeAttribute].Add(type);
	}

	private TypeSupport GetTypeSchemaDictionary(IEnumerable<Type> types,
		bool includeDependencies,
		IDictionary<SchemaTypeAttribute, List<Type>>? supported = null, 
		ICollection<UnsupportedType>? unsupported = null)
	{
		supported ??= new Dictionary<SchemaTypeAttribute, List<Type>>();
		unsupported ??= new List<UnsupportedType>();
		foreach (var type in types)
		{
			CheckSupport(type, includeDependencies, supported, unsupported);
		}
		return new TypeSupport(supported, unsupported);
	}

	private Exception? IsSupported(Type type)
	{
		if (type.IsGenericType)
		{
			return new ArgumentException("Generic types are not supported");
		}

		return null;
	}

	private static IReadOnlyCollection<IPocoStructure> ParseStructures(IDictionary<SchemaTypeAttribute, List<Type>> types)
	{
		var objectTypes = types
			.FirstOrDefault(x => x.Key is SchemaObjectAttribute, new KeyValuePair<SchemaTypeAttribute, List<Type>>(default!, new List<Type>()))
			.Value;

		var enumTypes = types
			.FirstOrDefault(x => x.Key is SchemaEnumAttribute, new KeyValuePair<SchemaTypeAttribute, List<Type>>(default!, new List<Type>()))
			.Value;

		var objects = objectTypes
			.Select(t =>
			{
				var validSchemaMembers = t.GetValidSchemaMembers();
				return new PocoObject(t, validSchemaMembers);
			})
			.Cast<IPocoStructure>()
			.ToArray();

		var enums = enumTypes
			.Select(t => new PocoEnum(t))
			.Cast<IPocoStructure>()
			.ToArray();
		
		return objects.Concat(enums).ToArray();
	}
}
