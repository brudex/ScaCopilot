using Microsoft.SemanticKernel;
using Newtonsoft.Json;

namespace ScaCopilot.SkFunctions;

public class FunctionList
{
     public static List<FunctionCallItem?>? AvailableFunctionCalls = new List<FunctionCallItem>();

     public static void Load()
     {
           var functionListFile = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "FunctionList.json");
          if(File.Exists(functionListFile))
          {
            var functionListJson = File.ReadAllText(functionListFile);
            AvailableFunctionCalls = JsonConvert.DeserializeObject<List<FunctionCallItem?>>(functionListJson);
          }
          
     }
     
     public static FunctionCallItem? GetFunctionCall(string functionName)
     {
         if (AvailableFunctionCalls != null)
         {
             return AvailableFunctionCalls.FirstOrDefault(x => x != null && x.function == functionName);
         }

         return null;
     }

}