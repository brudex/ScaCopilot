namespace ScaCopilot;
using Microsoft.SemanticKernel;
using System.ComponentModel;
using System.Globalization;

public class CompanySearchPlugin
{
    [KernelFunction,Description("search employee infomation")]
    public string EmployeeSearch(string input)
    {
        return "talk about hr information";
    }

    [KernelFunction,Description("search weather")]
    public string WeatherSearch(string text)
    {
        return text + ", 2 degree,rainy";
    }
}


public class PluginFunctionCalling
{
    
    public static async void Execute()
    {
        // Translate the content
        Console.WriteLine("PluginFunctionCalling Translating content...");
        var kernel = KernelConfig.GetKernelInstance();
        var companySearchPluginObj = new CompanySearchPlugin();
        var companySearchPlugin = kernel.ImportPluginFromObject(companySearchPluginObj, "CompanySearchPlugin");
        var weatherContent = await kernel.InvokeAsync( companySearchPlugin["WeatherSearch"],new(){["text"] = "guangzhou"});
        var result =  weatherContent.GetValue<string>();
        Console.WriteLine($"Weather Search result: {result}"); 
    }
    
}