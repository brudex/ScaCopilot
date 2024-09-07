using Microsoft.SemanticKernel;
using ScaCopilot.SkFunctions;

namespace ScaCopilot;

public class PromptServiceTest
{
    
    public static async void MenuDesign()
    {
        var kernel = KernelConfig.GetKernelInstance();
        var menuDesign = new MenDesign();
        kernel.ImportPluginFromObject(menuDesign, "MenuDesign");
        var plugin = kernel.GetSkill("UssdSkill");
        string input =
            $@"I want to build a USSD application for a mobile banking service with the following functionalities:
            1. Check Balance
            2. Transfer Funds
               - To Own Account
               - To Other Account
            3. Buy Airtime
               - For Self
               - For Others
            4. Mini Statement";
        var arguments = new KernelArguments() { ["input"] = input};
        var funcResult = await kernel.ExecuteFunction(plugin, "Expert", arguments);
        var result = funcResult.GetValue<string>();
        Console.WriteLine($"Translated content: {result}");
    }
    public static  void TestFunctionCalls(string appId)
    {
        var ussdApp = new UssdApp(appId);
        var result = Utils.ReadDataFile("promptResult.json");
        Console.WriteLine($"Prompt result >>> {result}"); 
        if (!string.IsNullOrEmpty(result))
        {
            ussdApp.ExecuteFunctionCalls(result);
        }
    }
    
    public static async  void Execute()
    {
        Console.WriteLine("Execute content...");
        string input =
            $@"I want to build a USSD application for a mobile banking service with the following functionalities:
                1. Check Balance
                2. Transfer Funds
                   - To Own Account
                   - To Other Account
                3. Buy Airtime
                   - For Self
                   - For Others
                4. Mini Statement";
        input = "Give me the menu text for all menus named betAmount";
        string appId = "asokoreMampong";
        TestFunctionCalls(appId);
        var result = await PromptService.Instance.ExecuteMenusInformationPrompt(appId, input);
        Console.WriteLine($"Translated content: {result}"); 
    }
}