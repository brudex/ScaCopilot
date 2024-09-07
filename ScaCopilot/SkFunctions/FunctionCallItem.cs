namespace ScaCopilot.SkFunctions;

public class FunctionCallItem
{
    public string endPoint { get; set; }
    public string function { get; set; }
    public string method { get; set; }
    
    public void Execute(string payload,string appId="")
    {
        Console.WriteLine("FunctionCallItem.Execute called with payload: " + payload);
        //call the function
        string url = $"{SettingsData.ScaApiUrl}{endPoint}";
        var restPostResponse = new RestPostResponse();
        if(method == "GET")
        {
            url += $"/{payload}";
            restPostResponse = RestHandler.Instance.AddJsonHeaders().AddCustomHeader("x-app-id",appId).DoGetString(url);
        }
        else if(method == "POST")
        {
            restPostResponse = RestHandler.Instance.AddJsonHeaders().AddCustomHeader("x-app-id",appId).DoPostGetString(url, payload);
        }
        Console.WriteLine("FunctionCallItem.Execute response: " + restPostResponse.content);
        Console.WriteLine("FunctionCallItem.Execute calling endPoint: " + endPoint + " function: " + function + " method: " + method);
    }
}