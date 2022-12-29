namespace AnotherProject.Seremonibasen.Models.Utility.Inputs;

[SchemaObject]
public record struct InputOptions
{
    public bool IsClearable { get; set; } = false;

    public bool Required { get; set; } = false;

    public InputOptions()
    { }
}
