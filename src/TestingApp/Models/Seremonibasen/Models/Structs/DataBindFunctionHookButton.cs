namespace TestingApp.Models.Seremonibasen.Models.Structs;

public sealed class DataBindFunctionHookButton
{
    public readonly Localization Text;

    // As FrontendFunctionHookNames is a static class, we cannot use it as a Func<T, string> constraint, which is very annoying..
    /// <summary>
    /// Use a reference to <see cref="Seremonibasen.Web.ViewModels.Utility.FrontendConstants.FrontendFunctionHookNames"/>
    /// </summary>
    public readonly string FunctionHookName;

    public DataBindFunctionHookButton(Localization text, string functionHookName)
    {
        Text = text;
        FunctionHookName = functionHookName;
    }
}
