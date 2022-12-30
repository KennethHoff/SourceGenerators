namespace AnotherProject.Seremonibasen.Models.Abstractions;

public interface IBasePocoObject
{
    Guid Id { get; set; }
    DataStatus? Status { get; set; }
}