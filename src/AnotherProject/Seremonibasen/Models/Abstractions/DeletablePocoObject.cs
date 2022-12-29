namespace AnotherProject.Seremonibasen.Models.Abstractions;

public abstract class DeletablePocoObject : BasePocoObject
{
    public bool? IsDeletable { get; set; }
    public bool IsDeleted { get; set; }
}
