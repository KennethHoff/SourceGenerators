namespace TestingApp.Models.Seremonibasen.Models.Utility.Inputs;

public record struct InputOptions
{
    public bool IsClearable { get; set; } = false;

    public bool Required { get; set; } = false;

    public InputOptions()
    { }
}
