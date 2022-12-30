﻿using Oxx.Backend.Generators.PocoSchema.Core.Configuration;
using Oxx.Backend.Generators.PocoSchema.Core.Configuration.Abstractions;
using Oxx.Backend.Generators.PocoSchema.Core.Configuration.Events;
using Oxx.Backend.Generators.PocoSchema.Core.Exceptions;
using Oxx.Backend.Generators.PocoSchema.Core.Logic.FileCreation;
using Oxx.Backend.Generators.PocoSchema.Core.Models.Files;
using Oxx.Backend.Generators.PocoSchema.Zod.Configuration;

namespace Oxx.Backend.Generators.PocoSchema.Zod;

internal sealed class ZodSchemaFileCreator : ISchemaFileCreator
{
	private readonly ISchemaConfiguration<ZodSchemaEvents> _configuration;

	public ZodSchemaFileCreator(ISchemaConfiguration<ZodSchemaEvents> configuration)
	{
		_configuration = configuration;
	}

	public Task CreateFilesAsync(IEnumerable<FileInformation> fileInformations)
		=> GenerateFilesAsync(fileInformations.ToArray());
	
	private async Task GenerateFilesAsync(IReadOnlyCollection<FileInformation> fileInformations)
	{
		EnsureDirectoryExists();
		
		_configuration.Events.FilesCreating?.Invoke(this, new FilesCreatingEventArgs(fileInformations));

		await Task.WhenAll(fileInformations.Select(CreateFileAsync));

		_configuration.Events.FilesCreated?.Invoke(this, new FilesCreatedEventArgs(fileInformations));
	}

	public async Task CreateFileAsync(FileInformation fileInformation)
	{
		var fileCreatingEventArgs = new FileCreatingEventArgs(fileInformation);
		_configuration.Events.FileCreating?.Invoke(this, fileCreatingEventArgs);

		if (!fileCreatingEventArgs.Skip)
		{
			var filePath = Path.Combine(_configuration.OutputDirectory.FullName, fileInformation.Name + _configuration.FullFileExtension);

			await File.WriteAllTextAsync(filePath, fileInformation.Content);
		}

		_configuration.Events.FileCreated?.Invoke(this, new FileCreatedEventArgs(fileInformation)
		{
			Skipped = fileCreatingEventArgs.Skip,
		});
	}

	private void EnsureDirectoryExists()
	{
		if (_configuration.FileDeletionMode is not FileDeletionMode.OverwriteExisting && _configuration.OutputDirectory.Exists)
		{
			var fileInfos = _configuration.OutputDirectory.GetFiles();

			var deletingFilesEventArgs = new DeletingFilesEventArgs(_configuration.OutputDirectory, fileInfos);
			_configuration.Events.DeletingFiles?.Invoke(this, deletingFilesEventArgs);

			if (_configuration.FileDeletionMode is FileDeletionMode.All)
			{
				EnsureOnlyAutoGeneratedFilesExist(_configuration.OutputDirectory, fileInfos);
			}
			
			foreach (var fileInfo in fileInfos)
			{
				fileInfo.Delete();
			}
		}

		_configuration.OutputDirectory.Create();
	}

	private void EnsureOnlyAutoGeneratedFilesExist(DirectoryInfo directoryInfo, IReadOnlyCollection<FileInfo> fileInfos)
	{
		var filesWithInvalidNaming = fileInfos.Where(x => !_configuration.FullFileNamingRegex.IsMatch(x.Name)).ToArray();
		if (filesWithInvalidNaming.Length is 0)
		{
			return;
		}

		var exception = new DirectoryContainsFilesWithIncompatibleNamingException(filesWithInvalidNaming);
		_configuration.Events.DeletingFilesFailed?.Invoke(this, new DeletingFilesFailedEventArgs(directoryInfo, fileInfos, exception));
		Environment.Exit(1);
	}
}