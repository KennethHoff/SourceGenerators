using System.Reflection;
using Oxx.Backend.Generators.PocoSchema.Core.Attributes;
using Oxx.Backend.Generators.PocoSchema.Core.Configuration;
using Oxx.Backend.Generators.PocoSchema.Core.Configuration.Events;
using Oxx.Backend.Generators.PocoSchema.Core.Extensions;
using Oxx.Backend.Generators.PocoSchema.Core.Models.Pocos;
using Oxx.Backend.Generators.PocoSchema.Core.Models.Pocos.Contracts;

namespace Oxx.Backend.Generators.PocoSchema.Core.Logic.PocoExtraction;

public class ConfiguredPocoStructureExtractor<TSchemaEvents> : IPocoStructureExtractor
	where TSchemaEvents : ISchemaEvents
{
	private readonly TSchemaEvents _events;

	public ConfiguredPocoStructureExtractor(ISchemaConfiguration<TSchemaEvents> configuration)
	{
		_events = configuration.Events;
	}

	public IPocoStructure Get<T>()
		=> Get(typeof(T));

	public IReadOnlyCollection<IPocoStructure> GetAllFromAssemblies(IReadOnlyCollection<Assembly> assemblies)
	{
		var (types, unsupportedTypes) = GetTypeSchemaDictionary(assemblies);

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

		var pocoStructures = objects.Concat(enums).ToArray();

		_events.PocoStructuresCreated?.Invoke(this, new PocoStructuresCreatedEventArgs(pocoStructures, unsupportedTypes));
		return pocoStructures;
	}

	public IPocoStructure Get(Type type)
		=> throw new NotImplementedException();

	public IReadOnlyCollection<IPocoStructure> Get(IEnumerable<Type> types)
		=> throw new NotImplementedException();

	private static (Dictionary<SchemaTypeAttribute, List<Type>> types, List<(Type Type, Exception Exception)> unsupportedTypes) GetTypeSchemaDictionary(IReadOnlyCollection<Assembly> assemblies)
	{
		var types = new Dictionary<SchemaTypeAttribute, List<Type>>();
		var unsupportedTypes = new List<(Type Type, Exception Exception)>();
		foreach (var assembly in assemblies)
		{
			foreach (var type in assembly.GetTypes())
			{
				var schemaTypeAttribute = type.GetCustomAttribute<SchemaTypeAttribute>();
				if (schemaTypeAttribute is null)
				{
					continue;
				}

				if (IsSupported(type) is { } exception)
				{
					unsupportedTypes.Add((type, exception));
					// _configuration.Events.UnsupportedTypeFound?.Invoke(this, new UnsupportedTypeFoundEventArgs(type, exception));
					continue;
				}

				if (!types.ContainsKey(schemaTypeAttribute))
				{
					types.Add(schemaTypeAttribute, new List<Type>());
				}

				types[schemaTypeAttribute].Add(type);
			}
		}

		return (types, unsupportedTypes);
	}

	private static Exception? IsSupported(Type type)
	{
		if (type.IsGenericType)
		{
			return new ArgumentException("Generic types are not supported");
		}

		var validMemberInfos = type.GetValidSchemaMembers().ToArray();
		if (!validMemberInfos.Any())
		{
			return new ArgumentException("No fields or properties found");
		}

		return null;
	}

}