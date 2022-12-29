﻿using System.Linq.Expressions;

namespace TestingApp.Models.Seremonibasen.Models.Utility.Inputs;

public class KnockoutTextAreaOptions
{
    public KnockoutTextAreaOptions(string targetProperty)
    {
        TargetProperty = targetProperty;
    }

    public Localization Localization { get; set; }
    public string TargetProperty { get; }
    public Localization PlaceholderLocalization { get; set; }
}
