using Microsoft.SemanticKernel;

namespace ScaCopilot;

public static class KernelExtensions
{
    public static KernelPlugin GetSkill(this Kernel kernel,string skillName)
    {
        Console.WriteLine("Getting Skill: >>> " + skillName);
        var skillsDirectory = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "skills");
        Console.WriteLine("The skills directory is: >>> " + skillsDirectory);
        var promptDirectory = Path.Combine(skillsDirectory, skillName);
        Console.WriteLine("Prompt Directory: >>> " + promptDirectory);
        var mySkill = kernel.CreatePluginFromPromptDirectory(promptDirectory);
        return mySkill;
    }
    
    
    public static KernelArguments GetKernelArguments(this Kernel kernel,Dictionary<string,string> contextData)
    {
        var myContext = new KernelArguments(); 
        foreach(var item in contextData)
        {
            myContext.Add(item.Key, item.Value); 
        }
        return myContext;
    }
    
    public static async  Task<FunctionResult>  ExecuteFunction(this Kernel kernel, KernelPlugin skill,string functionName, KernelArguments? context=null)
    { 
        Logger.Info("KernelExtensions", "ExecuteFunction : >>> " + functionName);
        if(context == null)
        {
            context = new KernelArguments();
        }
        var result = await kernel.InvokeAsync(skill[functionName], context);
        return result;
    }
}