namespace TestingApp.Models.Seremonibasen.Models.Abstractions;

[SchemaObject]
public abstract class DeletablePocoObject : BasePocoObject
{
    public bool? IsDeletable { get; set; }
    public bool IsDeleted { get; set; }
}
