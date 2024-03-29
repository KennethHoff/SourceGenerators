﻿using AnotherProject;
using AnotherProject.Seremonibasen.Models;
using Oxx.Backend.Generators.PocoSchema.Core;
using Oxx.Backend.Generators.PocoSchema.Core.Configuration.Abstractions;
using Oxx.Backend.Generators.PocoSchema.Core.Configuration.Events;
using Oxx.Backend.Generators.PocoSchema.Core.Exceptions;
using Oxx.Backend.Generators.PocoSchema.Zod;
using Oxx.Backend.Generators.PocoSchema.Zod.Configuration;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.BuiltIn;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Custom;
using TestingApp.Models;
using TestingApp.SchemaTypes;

namespace TestingApp;

internal static class TestingAppSchemaGenerationConfiguration
{
	public static async Task GenerateSchemaAsync()
	{
		var configuration = new ZodSchemaConfigurationBuilder()
			.SetDirectories(x =>
			{
				x.Root = "C:/OXX/Projects/Suppehue/Suppehue.Frontend.NextJS/src/zod/";
				x.Enums = "enums/";
				x.Atoms = "atoms/";
				x.Molecules = "molecules/";
				x.TypeScript = new TypeScriptConfiguration
				{
					Alias = "@/zod/",
				};
			})
			.OverrideFileDeletionMode(FileDeletionMode.All)
			.OverrideFileNameNamingFormat("{0}Schema")
			.ResolveTypesFromAssemblyContaining<ITestingAppAssemblyMarker>()
			.ResolveTypesFromAssemblyContaining<IAnotherProjectAssemblyMarker>()
			.ApplyAtomicSchema<Localization, StringBuiltInAtomicZodSchema>()
			.ApplyAtomicSchema<PersonId, TypedIdAtomicZodSchema<PersonId>>()
			.ApplyAtomicSchema<CeremonyId, TypedIdAtomicZodSchema<CeremonyId>>()
			.ApplyAtomicSchema<ClampedNumber, ClampedNumberAtomicZodSchema>(() => new ClampedNumberAtomicZodSchema(..10))
			.ConfigureEvents(events =>
			{
				events.PocoStructuresCreated += (_, args) => PocoStructuresCreatedEventHandler(args);
				events.MoleculeSchemasCreated += (_, args) => MoleculeSchemasCreatedEventHandler(args);
				events.GenerationCompleted += (_, args) => GenerationCompletedEventHandler(args);
				events.GenerationStarted += (_, args) => GenerationStartedEventHandler(args);
				events.DeletingFiles += (_, args) => DeletingFilesEventHandler(args);
				events.DeletingFilesFailed += (_, args) => DeletingFilesFailedEventHandler(args);
			})
			.Build();

		ISchemaGenerator generator = new ZodSchemaGenerator(configuration);
		await generator.GenerateAllAsync();
		// await generator.GenerateAsync<ArrayTests>();
	}
	private static void MoleculeSchemasCreatedEventHandler(MoleculeSchemasCreatedEventArgs eventArgs)
	{
		var informations = eventArgs.Informations;

		var informationsWithInvalidMembers = informations
			.Where(x => x.InvalidMembers.Any())
			.ToArray();
		
		switch (informationsWithInvalidMembers.Length)
		{
			case 0:
				PrintAllSchemasResolved();
				break;
			default:
				PrintTotal();
				PrintTypesWithoutSchemas();
				break;
		}
		
		

		Console.WriteLine();

		void PrintTotal()
		{
			var invalidTypesAmount = informationsWithInvalidMembers.Length;
			ColoredConsole.Write(informations.Count, ConsoleColor.Cyan);
			Console.Write(" non-atoms were resolved, of which ");
			ColoredConsole.Write(informations.Count - invalidTypesAmount, ConsoleColor.Green);
			Console.WriteLine(" were resolved fully.");
		}

		void PrintTypesWithoutSchemas()
		{
			var typesWithoutSchemas = informations
				.SelectMany(information => information.InvalidMembers)
				.Select(memberInfo => memberInfo.Type)
				.Distinct()
				.ToArray();

			Console.Write("The following ");
			ColoredConsole.Write(typesWithoutSchemas.Length, ConsoleColor.Red);
			Console.Write(" schemas could not be resolved in ");
			ColoredConsole.Write(informationsWithInvalidMembers.Length, ConsoleColor.Cyan);
			Console.WriteLine(" type-schemas:");
			foreach (var type in typesWithoutSchemas)
			{
				var typesContainingSchema = informations
					.Where(information => information.InvalidMembers.Any(memberInfo => memberInfo.Type == type))
					.Select(information => information.Type)
					.ToArray();

				var firstTwoTypes = typesContainingSchema.Take(2).ToArray();
				var length = typesContainingSchema.Length;
				ColoredConsole.WriteLine(type + " (" + length + "): " + string.Join(", ", firstTwoTypes.Select(x => x.Name)) + (length > 2
					? ", ..."
					: ""), ConsoleColor.Cyan);
			}
			Console.WriteLine("Keep in mind that generics might have failed to resolve due to their type arguments being unable to be resolved.");
		}

		void PrintAllSchemasResolved()
		{
			Console.Write("All ");
			ColoredConsole.Write(informations.Count, ConsoleColor.Green);
			Console.WriteLine(" type-schemas were created successfully.");
		}
	}

	private static void PocoStructuresCreatedEventHandler(PocoStructuresCreatedEventArgs eventArgs)
	{
		var unsupportedTypes = eventArgs.UnsupportedTypes;

		switch (unsupportedTypes.Count)
		{
			case 0:
				PrintAllTypesResolved();
				break;
			default:
				PrintSuccessfulStructures();
				PrintUnsupportedTypes();
				break;
		}

		Console.WriteLine();


		void PrintUnsupportedTypes()
		{
			Console.Write("The following ");
			ColoredConsole.Write(unsupportedTypes.Count, ConsoleColor.Red);
			Console.WriteLine(" types could not be resolved:");
			foreach (var (type, exception) in unsupportedTypes)
			{
				ColoredConsole.Write(type, ConsoleColor.Yellow);
				Console.Write(" (");
				ColoredConsole.Write(exception.Message, ConsoleColor.Magenta);
				Console.WriteLine(")");
			}
			Console.WriteLine();
		}

		void PrintSuccessfulStructures()
		{
			Console.Write("Successfully resolved ");
			ColoredConsole.Write(eventArgs.PocoStructures.Count, ConsoleColor.Green);
			Console.Write(" out of ");
			ColoredConsole.Write(eventArgs.PocoStructures.Count + unsupportedTypes.Count, ConsoleColor.Cyan);
			Console.WriteLine(" types.");
		}

		void PrintAllTypesResolved()
		{
			Console.Write("All ");
			ColoredConsole.Write(eventArgs.PocoStructures.Count, ConsoleColor.Green);
			Console.WriteLine(" types were resolved successfully.");
		}
	}
	
	private const string DateTimeFormat = "HH:mm:ss.fff";

	private static void GenerationStartedEventHandler(GenerationStartedEventArgs eventArgs)
	{
		Console.Write("Started generating at ");
		ColoredConsole.Write(eventArgs.GenerationStartedTime.ToString(DateTimeFormat), ConsoleColor.Cyan);
		Console.WriteLine(".");
		Console.WriteLine();
	}

	private static void GenerationCompletedEventHandler(GenerationCompletedEventArgs eventArgs)
	{
		var duration = eventArgs.GenerationCompletedTime - eventArgs.GenerationStartedTime;

		Console.Write("Generation completed at ");
		ColoredConsole.Write(eventArgs.GenerationCompletedTime.ToString(DateTimeFormat), ConsoleColor.Cyan);
		Console.Write(" after ");
		ColoredConsole.Write(duration.TotalMilliseconds.ToString("0"), ConsoleColor.Cyan);
		Console.WriteLine(" ms.");
		Console.WriteLine();
	}

	private static void DeletingFilesEventHandler(DeletingFilesEventArgs eventArgs)
	{
		Console.WriteLine("Deleting directories:");
		var hmm = eventArgs.Dictionary.Select(x => x);
		foreach (var (directory, files) in hmm)
		{
			PrintDirectory(directory, files);
		}

		void PrintDirectory(DirectoryInfo directory, IEnumerable<FileInfo> files)
		{
			Console.Write("'");
			ColoredConsole.Write(directory.FullName, ConsoleColor.Cyan);
			Console.Write("' (");
			ColoredConsole.Write(files.Count().ToString(), ConsoleColor.Cyan);
			Console.WriteLine(" files)");
		}
	}

	private static void DeletingFilesFailedEventHandler(DeletingFilesFailedEventArgs eventArgs)
	{
		ColoredConsole.Write("Failed to delete directory ", ConsoleColor.Red);
		ColoredConsole.Write(eventArgs.Directory, ConsoleColor.Cyan);
		ColoredConsole.Write(" containing ", ConsoleColor.Red);
		ColoredConsole.Write(eventArgs.Files.Count, ConsoleColor.Cyan);
		ColoredConsole.WriteLine(" files:", ConsoleColor.Red);
		
		if (eventArgs.Exception is DirectoryContainsFilesWithIncompatibleNamingException directoryContainsFilesWithIncompatibleNamingException)
		{
			Console.WriteLine("The following files have incompatible naming. " +
							  "This is most likely due to the files being added or renamed manually, or the naming convention being changed. " + 
							  Environment.NewLine +
							  "If you want to regenerate them, you will have to delete them manually, " +
							  $"or change {nameof(FileDeletionMode)} to {nameof(FileDeletionMode.ForcedAll)} or {nameof(FileDeletionMode.OverwriteExisting)}");
			foreach (var file in directoryContainsFilesWithIncompatibleNamingException.FilesWithInvalidFileExtensions)
			{
				ColoredConsole.WriteLine(file, ConsoleColor.Red);
			}
		}

		Console.WriteLine();
	}
}