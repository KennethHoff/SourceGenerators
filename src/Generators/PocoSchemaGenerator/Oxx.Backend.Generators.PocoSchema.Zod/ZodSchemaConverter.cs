using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using Oxx.Backend.Generators.PocoSchema.Core.Contracts;
using Oxx.Backend.Generators.PocoSchema.Core.Models;
using Oxx.Backend.Generators.PocoSchema.Zod.Configuration;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Abstractions;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts.Models;

namespace Oxx.Backend.Generators.PocoSchema.Zod;

public class ZodSchemaConverter : ISchemaConverter
{
	private static readonly string StandardHeader = $$"""
		// This file is autogenerated by Oxx.Backend.Generators.PocoSchema.Zod
		// Version: {{typeof(ZodSchemaConverter).Assembly.GetName().Version}}
		// Date: {{DateOnly.FromDateTime(DateTime.Now).ToString("O", DateTimeFormatInfo.InvariantInfo)}}
		// Do not edit this file manually

		import { z } from "zod";
		""";

	private readonly ZodSchemaConfiguration _configuration;

	private readonly TypeSchemaDictionary<IPartialZodSchema> _generatedSchemas = new();

	public ZodSchemaConverter(ZodSchemaConfiguration configuration)
	{
		_configuration = configuration;
	}

	#region Interface implementations

	public IEnumerable<FileInformation> GenerateFileContent(IEnumerable<PocoObject> pocoObjects)
	{
		var atoms = GenerateAtoms(_configuration.AppliedSchemaDictionary);
		var molecules = GenerateMolecules(pocoObjects);
		return atoms.Concat(molecules).Where(x => x != FileInformation.None);
	}

	#endregion

	private FileInformation GenerateAtom(KeyValuePair<Type, IPartialZodSchema> atomicSchema)
	{
		_generatedSchemas.Add(atomicSchema.Key, atomicSchema.Value);

		if (atomicSchema.Value is IBuiltInAtomicZodSchema)
		{
			return FileInformation.None;
		}

		return new FileInformation
		{
			Content = GenerateFileContent(atomicSchema.Value),
			Name = GenerateFileName(atomicSchema.Value),
		};
	}

	private FileContent GenerateAtomicFileContent(IAtomicZodSchema atomicZodSchema)
		=> new($$"""
		{{StandardHeader}}

		export const {{_configuration.FormatSchemaName(atomicZodSchema)}} = {{atomicZodSchema.SchemaDefinition}};

		export type {{_configuration.FormatSchemaTypeName(atomicZodSchema)}} = z.infer<typeof {{_configuration.FormatSchemaName(atomicZodSchema)}}>;

		""");

	private IEnumerable<FileInformation> GenerateAtoms(TypeSchemaDictionary<IPartialZodSchema> configurationAtomicSchemaDictionary)
		=> configurationAtomicSchemaDictionary
			.Select(GenerateAtom)
			.ToArray();

	private FileContent GenerateFileContent(IPartialZodSchema schemaValue)
		=> schemaValue switch
		{
			IAtomicZodSchema atomicZodSchema       => GenerateAtomicFileContent(atomicZodSchema),
			IMolecularZodSchema molecularZodSchema => GenerateMolecularFileContent(molecularZodSchema),
			_                                      => FileContent.None,
		};

	private FileName GenerateFileName(IPartialZodSchema schemaValue)
		=> new(string.Format(_configuration.SchemaFileNameFormat, schemaValue.SchemaBaseName));

	private FileContent GenerateMolecularFileContent(IMolecularZodSchema molecularZodSchema)
		=> new($$"""
		{{StandardHeader}}
		{{molecularZodSchema.AdditionalImportsString}}
		
		export const {{_configuration.FormatSchemaName(molecularZodSchema)}} = {{molecularZodSchema.SchemaDefinition}};

		export type {{_configuration.FormatSchemaTypeName(molecularZodSchema)}} = z.infer<typeof {{_configuration.FormatSchemaName(molecularZodSchema)}}>;

		""");

	private IMolecularZodSchema GenerateMolecularSchema(PocoObject pocoObject)
	{
		var partialSchema = _generatedSchemas[pocoObject.Type] switch
		{
			PartialMolecularZodSchema schema => schema,
			IZodSchema schema => throw new UnreachableException(
				$"Unexpected schema type. {pocoObject.TypeName} has already been generated as {schema.GetType().Name}"),
			_ => throw new UnreachableException("Schema should have been generated before this point"),
		};

		var validSchemas = pocoObject.Properties
			.Where(x =>
			{
				var propertyType = x.PropertyType;

				if (_generatedSchemas.HasSchemaForType(propertyType))
				{
					return true;
				}

				// If the propertyType is generic, we need to get the generic type definition
				if (propertyType.IsGenericType)
				{
					var hasRelatedType = _configuration.GenericSchemaDictionary.HasRelatedType(propertyType.GetGenericTypeDefinition());
					var allGenericArgumentsHaveSchema = propertyType.GetGenericArguments().All(_generatedSchemas.HasSchemaForType);
					return hasRelatedType && allGenericArgumentsHaveSchema;
				}

				return false;
			})
			.ToArray()
			.Select(x =>
			{
				var propertyType = x.PropertyType;

				var partialZodSchema = _generatedSchemas.GetSchemaForType(propertyType);
				if (partialZodSchema is not null)
				{
					return KeyValuePair.Create(x, partialZodSchema);
				}

				// If the propertyType is generic, we need to get the generic type definition
				if (propertyType.IsGenericType)
				{
					var genericSchema = _configuration.CreateGenericSchema(x);
					return KeyValuePair.Create(x, genericSchema);
				}

				throw new InvalidOperationException($"No schema found for {propertyType.Name}");
			})
			.ToDictionary(x => x.Key, x => x.Value);

		return partialSchema.Populate(validSchemas, _configuration);
	}

	private FileInformation GenerateMolecule(PocoObject pocoObject)
	{
		var molecularSchema = GenerateMolecularSchema(pocoObject);
		
		_generatedSchemas.Update(pocoObject.Type, molecularSchema);

		return new FileInformation
		{
			Content = GenerateFileContent(molecularSchema),
			Name = GenerateFileName(molecularSchema),
		};
	}

	/// <summary>
	///     In order to prevent circular dependencies, we need to generate the molecule definitions first.
	/// </summary>
	private PocoObject GenerateMoleculeDefinition(PocoObject pocoObject)
	{
		if (!_generatedSchemas.ContainsKey(pocoObject.Type))
		{
			_generatedSchemas.Add(pocoObject.Type, new PartialMolecularZodSchema
			{
				SchemaBaseName = new SchemaBaseName(pocoObject.TypeName),
			});
		}

		return pocoObject;
	}

	private IEnumerable<FileInformation> GenerateMolecules(IEnumerable<PocoObject> pocoObjects)
	{
		var definitions = pocoObjects
			.Select(GenerateMoleculeDefinition)
			.ToArray();
		
		_configuration.SchemaDictionary = _generatedSchemas;
		
		return definitions
			.Select(GenerateMolecule)
			.ToArray();
	}
}