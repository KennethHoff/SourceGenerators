namespace AnotherProject.Seremonibasen.Models.Abstractions;

[SchemaEnum]
public enum DataStatus
{
	NO_CHANGE = 1,
	NEW = 2,
	UPDATED = 3,
	DELETED = 4,
	INDEX_CHANGE = 5,
}