namespace AnotherProject.Seremonibasen.Models.Abstractions;

public interface IHasAccessToContent
{
	bool HasDeleteAccess { get; set; }
	bool HasReadAccess { get; set; }
	bool HasWriteAccess { get; set; }
}