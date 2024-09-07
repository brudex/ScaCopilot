using Microsoft.SemanticKernel;

namespace ScaCopilot;

public class TranslateContentSkill
{
     
    public static async  void Execute()
    {
        // Translate the content
        Console.WriteLine("Translating content...");
        var kernel = KernelConfig.GetKernelInstance();
        var plugin = kernel.GetSkill("TranslatePlugin");
        var translateContent = await kernel.ExecuteFunction(plugin, "Basic", new(){["input"] = "你好，我是你的 AI 编排助手 - Semantic Kernel"});
        var result = translateContent.GetValue<string>();
        Console.WriteLine($"Translated content: {result}"); 
    }
}