using System.Reflection;
using Oxx.Backend.Generators.PocoSchema.Core.Attributes;
using Oxx.Backend.Generators.PocoSchema.Core.Configuration;
using Oxx.Backend.Generators.PocoSchema.Core.Configuration.Events;
using Oxx.Backend.Generators.PocoSchema.Core.Extensions;
using Oxx.Backend.Generators.PocoSchema.Core.Models.Pocos;
using Oxx.Backend.Generators.PocoSchema.Core.Models.Pocos.Contracts;
using Oxx.Backend.Generators.PocoSchema.Core.Models.Types;

namespace Oxx.Backend.Generators.PocoSchema.Core.Logic.PocoExtraction;

public abstract class ConfiguredPocoStructureExtractor<TSchemaConfiguration, TSchemaEvents, TDirectoryOutputConfiguration> : IPocoStructureExtractor
	where TSchemaConfiguration: ISchemaConfiguration<TSchemaEvents, TDirectoryOutputConfiguration>
	where TSchemaEvents : ISchemaEvents
	where TDirectoryOutputConfiguration: IDirectoryOutputConfiguration
{
	protected readonly TSchemaConfiguration Configuration;

	protected ConfiguredPocoStructureExtractor(TSchemaConfiguration configuration)
	{
		Configuration = configuration;
	}

	#region Interface implementations

	public abstract IReadOnlyCollection<IPocoStructure> Get(IEnumerable<Type> requestedTypes, bool includeDependencies = true);

	public abstract IReadOnlyCollection<IPocoStructure> GetAll();

	#endregion

	private void CheckSupport(Type type, bool includeDependencies, TypeCollectionTypeDictionary supported, ICollection<UnsupportedType> unsupported)
	{
		if (unsupported.Any(x => x.Type == type))
		{
			return;
		}

		// if is generic, check if all generic arguments are supported
		if (type.IsGenericType)
		{
			CheckSupportForGenerics(type, includeDependencies, supported, unsupported);
		}
		
		var schemaTypeAttribute = type.GetCustomAttribute<SchemaTypeAttribute>();
		if (schemaTypeAttribute is null)
		{
			return;
		}

		var pocoStructure = schemaTypeAttribute.UnderlyingType;
		if (!supported.ContainsKey(pocoStructure))
		{
			supported.Add(pocoStructure, new List<Type>());
		}

		if (supported[pocoStructure].Contains(type))
		{
			return;
		}

		if (includeDependencies)
		{
			CheckSupportForDependencies(type, includeDependencies, supported, unsupported);
		}

		if (IsSupported(type) is { } exception)
		{
			unsupported.Add(new UnsupportedType(type, exception));
			// _configuration.Events.UnsupportedTypeFound?.Invoke(this, new UnsupportedTypeFoundEventArgs(type, exception));
			return;
		}

		supported[pocoStructure].Add(type);
	}

	private void CheckSupportForDependencies(Type type, bool includeDependencies, TypeCollectionTypeDictionary supported, ICollection<UnsupportedType> unsupported)
	{
		foreach (var member in type.GetValidSchemaMembers())
		{
			CheckSupport(member.Type, includeDependencies, supported, unsupported);
		}
	}

	private void CheckSupportForGenerics(Type type, bool includeDependencies, TypeCollectionTypeDictionary supported, ICollection<UnsupportedType> unsupported)
	{
		var genericArguments = type.GetGenericArguments();
		foreach (var genericArgument in genericArguments)
		{
			CheckSupport(genericArgument, includeDependencies, supported, unsupported);
		}
	}

	protected TypeSupport GetTypeSchemaDictionary(IEnumerable<Type> types,
		bool includeDependencies,
		TypeCollectionTypeDictionary? supported = null, 
		ICollection<UnsupportedType>? unsupported = null)
	{
		supported ??= new TypeCollectionTypeDictionary();
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

	protected static IReadOnlyCollection<IPocoStructure> ParseStructures(TypeCollectionTypeDictionary typesCollection)
	{
		var objectTypes = typesCollection
			.FirstOrDefault(x => x.Key == typeof(PocoObject), new KeyValuePair<Type, List<Type>>(default!, new List<Type>()))
			.Value;

		var enumTypes = typesCollection
			.FirstOrDefault(x => x.Key == typeof(PocoEnum), new KeyValuePair<Type, List<Type>>(default!, new List<Type>()))
			.Value;
		
		var atomTypes = typesCollection
			.FirstOrDefault(x => x.Key == typeof(PocoAtom), new KeyValuePair<Type, List<Type>>(default!, new List<Type>()))
			.Value;

		var atoms = atomTypes
			.Select(t => new PocoAtom(t))
			.Cast<IPocoStructure>()
			.ToArray();

		var enums = enumTypes
			.Select(t => new PocoEnum(t))
			.Cast<IPocoStructure>()
			.ToArray();

		var objects = objectTypes
			.Select(t =>
			{
				var validSchemaMembers = t.GetValidSchemaMembers();
				return new PocoObject(t, validSchemaMembers);
			})
			.Cast<IPocoStructure>()
			.ToArray();
		
		return atoms
			.Concat(enums)
			.Concat(objects)
			.ToArray();
	}
}
