namespace TestingApp.Models.Seremonibasen.Models.Utility.Select;

public record struct DisabledEnumSelectOption<TEnum>(TEnum Value, string DisableReason = "") where TEnum : Enum;
