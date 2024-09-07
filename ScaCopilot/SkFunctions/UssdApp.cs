using Newtonsoft.Json.Linq;

namespace ScaCopilot.SkFunctions;

public class UssdApp
{
    public string AppId { get; set; }
    public string MenusJson { get; set; }
    public UssdApp(string appId)
    {
        AppId = appId;
    } 
    
    public string GetAllMenus()
    {
        MenusJson= "[]";
        Console.WriteLine("UssdApp.GetAllMenus called");
        var restPostResponse = new RestPostResponse();
        string url = $"{SettingsData.ScaApiUrl}/api/getAllMenus/{AppId}";
        restPostResponse = RestHandler.Instance.AddJsonHeaders().AddCustomHeader("x-app-id",AppId).DoGetString(url);
        Console.WriteLine("UssdApp.GetAllMenus response: " + restPostResponse.content);
        if (restPostResponse.IsSuccessStatus())
        {
            MenusJson = restPostResponse.content;
        }
        return MenusJson;
    }
    
    public void ExecuteFunctionCalls(string functionCallPlan)
    {
        Console.WriteLine("UssdApp.ExecuteFunctionCalls called with functionCallPlan: " + functionCallPlan);
        var jsonArray = JArray.Parse(functionCallPlan);
        for(int i = 0; i < jsonArray.Count; i++)
        {
            var item = jsonArray[i];
            var functionName = item["function"].ToString();
            var payload = item["argument"].ToString();
            var functionCall = FunctionList.GetFunctionCall(functionName);
            if(functionCall != null)
            {
                functionCall.Execute(payload,AppId);
            }
            Logger.Info(this,$"Function {functionName} executed with payload: {payload}");
            Console.WriteLine("UssdApp.ExecuteFunctionCalls calling function: " + functionName + " with payload: " + payload);
        }
    }
    
    public void ExecuteActionDesignCalls(string promptResult)
    {
        Console.WriteLine("UssdApp.ExecuteActionDesignCalls called with functionCallPlan: " + promptResult);
         string functionName = "UpdateActionsForMenus";
        var functionCall = FunctionList.GetFunctionCall(functionName);
        if(functionCall != null)
        {
            functionCall.Execute(promptResult,AppId);
        }
        Logger.Info(this,$"Function {functionName} executed with payload: {promptResult}");
        Console.WriteLine("UssdApp.ExecuteFunctionCalls calling function: " + functionName + " with payload: " + promptResult);
        
    }
    
    
}