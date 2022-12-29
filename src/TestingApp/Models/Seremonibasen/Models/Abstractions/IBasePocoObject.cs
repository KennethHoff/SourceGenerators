namespace TestingApp.Models.Seremonibasen.Models.Abstractions;

public interface IBasePocoObject
{
    Guid Id { get; set; }
    DataStatus? Status { get; set; }
}

[SchemaEnum]
public enum DataStatus
{
    NO_CHANGE = 1,
    NEW = 2,
    UPDATED = 3,
    DELETED = 4,
    INDEX_CHANGE = 5,
}
