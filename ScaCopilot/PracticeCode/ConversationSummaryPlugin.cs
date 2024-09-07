using HandlebarsDotNet.Helpers.Utils;
using Microsoft.SemanticKernel;

namespace ScaCopilot;

public class ConversationSummaryPlugin
{
    public static async void Execute()
    {
         Console.WriteLine("Importing native plugins content...");
        var kernel = KernelConfig.GetKernelInstance();
        kernel.ImportPluginFromType<ConversationSummaryPlugin>();
        string input = @"I'm a vegan in search of new recipes. I love spicy food! Can you give me a list of breakfast recipes that are vegan friendly?";
        var result = await kernel.InvokeAsync(
            "ConversationSummaryPlugin", 
            "GetConversationActionItems", 
            new() {{ "input", input }});
        Console.WriteLine($"ConversationSummaryPlugin result: {result}"); 
    }
}