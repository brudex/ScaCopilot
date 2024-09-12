using Microsoft.SemanticKernel;
using ScaCopilot.Models;
using ScaCopilot.SkFunctions;

namespace ScaCopilot;

public class PromptService
{
    //singleton
    private static PromptService _instance;
    public static PromptService Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new PromptService();
            }
            return _instance;
        }
    }

    

    public async Task<PromptResponse> ExecuteMenuExpertPrompt(string appId, string userInput)
    {
        Console.WriteLine("Executing ExecuteMenuExpertPrompt...");
        var response = new PromptResponse();
        response.status = PromptResponse.StatusCodesPromptError;
        var kernel = KernelConfig.GetKernelInstance();
        var plugin = kernel.GetSkill("UssdSkill");
        var ussdApp = new UssdApp(appId);
        string menusJson = ussdApp.GetAllMenus();
        string result = string.Empty;
        try
        {
            var translateContent = await kernel.ExecuteFunction(plugin, "MenuCrud",
                new() { ["input"] = userInput, ["menus"] = menusJson });
              result = translateContent.GetValue<string>();
            Console.WriteLine($"Prompt result >>> {result}");
            if (!string.IsNullOrEmpty(result))
            {
                ussdApp.ExecuteFunctionCalls(result);
            }
            response.status = PromptResponse.StatusCodesSuccess;
            response.message = result;
            return response;
        }
        catch (HttpOperationException ex)
        {
            Logger.Error(typeof(PromptService), "Error connecting to ai service " + ex.Message,ex);
            Console.WriteLine("Error connecting to ai service " + ex.Message);
            result = "Error connecting to ai service " + ex.Message;
            response.status = PromptResponse.StatusCodesModelUnreachable;
            response.message = result;
        }
        catch (Exception ex)
        {
            Logger.Error(typeof(PromptService), "Error connecting to ai service " + ex.Message,ex);
            Console.WriteLine("Error connecting to ai service " + ex.Message);
            result = "Error connecting to ai service " + ex.Message;
            response.status = PromptResponse.StatusCodesException;
            response.message = result;
        }
        return response;
    }
    
    
    public async Task<PromptResponse> ExecuteActionDesignPrompt(string appId, string userInput)
    {
        Console.WriteLine("Executing ExecuteActionDesignPrompt...");
        var response = new PromptResponse();
        response.status = PromptResponse.StatusCodesPromptError;
        var kernel = KernelConfig.GetKernelInstance();
        var plugin = kernel.GetSkill("UssdSkill");
        var ussdApp = new UssdApp(appId);
        string menusJson = ussdApp.GetAllMenus();
        string result = string.Empty;
        try
        {
            var translateContent = await kernel.ExecuteFunction(plugin, "ActionDesign",
                new() { ["input"] = userInput, ["menus"] = menusJson });
            result = translateContent.GetValue<string>();
            Console.WriteLine($"Prompt result >>> {result}");
            if (!string.IsNullOrEmpty(result))
            {
                ussdApp.ExecuteActionDesignCalls(result);
            }
            response.status = PromptResponse.StatusCodesSuccess;
            response.message = result;
            return response;
        }
        catch (HttpOperationException ex)
        {
            Logger.Error(typeof(PromptService), "Error connecting to ai service " + ex.Message,ex);
            Console.WriteLine("Error connecting to ai service " + ex.Message);
            result = "Error connecting to ai service " + ex.Message;
            response.status = PromptResponse.StatusCodesModelUnreachable;
            response.message = result;
        }
        catch (Exception ex)
        {
            Logger.Error(typeof(PromptService), "Error connecting to ai service " + ex.Message,ex);
            Console.WriteLine("Error connecting to ai service " + ex.Message);
            result = "Error connecting to ai service " + ex.Message;
            response.status = PromptResponse.StatusCodesException;
            response.message = result;
        }
        return response;
    }
    
    
    public  async Task<PromptResponse> ExecuteMenusInformationPrompt(string appId, string userInput)
    {
        Console.WriteLine("Executing ExecuteMenuExpertPrompt...");
        var response = new PromptResponse();
        response.status = PromptResponse.StatusCodesPromptError;
        Logger.Info(typeof(PromptService), "Executing ExecuteMenuExpertPrompt...");
        var kernel = KernelConfig.GetKernelInstance();
        var plugin = kernel.GetSkill("UssdSkill");
        var ussdApp = new UssdApp(appId);
        string menusJson = ussdApp.GetAllMenus();
        string result = string.Empty;
        try
        {
            var translateContent = await kernel.ExecuteFunction(plugin, "RetrieveInfo",
                new() { ["input"] = userInput, ["menus"] = menusJson });
            result = translateContent.GetValue<string>();
            response.status = PromptResponse.StatusCodesSuccess;
            response.message = result;
            return response;
        }
        catch (HttpOperationException ex)
        {
            Logger.Error(typeof(PromptService), "Error connecting to ai service " + ex.Message,ex);
            Console.WriteLine("Error connecting to ai service " + ex.Message);
            result = "Error connecting to ai service " + ex.Message;
            response.status = PromptResponse.StatusCodesModelUnreachable;
            response.message = result;
        }
        catch (Exception ex)
        {
            Logger.Error(typeof(PromptService), "Error connecting to ai service " + ex.Message,ex);
            Console.WriteLine("Error connecting to ai service " + ex.Message);
            result = "Error connecting to ai service " + ex.Message;
            response.status = PromptResponse.StatusCodesException;
            response.message = result;
        }
        return response;
    }
     
    
    public async Task<CopilotResponse>  Execute(CopilotRequest request)
    {
        Console.WriteLine("Prompt request received>>>>..."+request.ToJsonString());
        Logger.Info(this,"Prompt request received>>>>..."+request.ToJsonString());
        string input = request.prompt;
        var promptResponse = await ExecuteMenusInformationPrompt(request.appId, input);
        Console.WriteLine("Prompt result >>>"+promptResponse.message);
        Logger.Info(this,"Prompt result >>>"+promptResponse.message);
        var response = new CopilotResponse();
        response.status = promptResponse.status;
        response.message = promptResponse.message;
        return response;

    }

    //
    // public async Task<CopilotResponse> Cordinate(CopilotRequest request)
    // {
    //     Console.WriteLine("Prompt request received>>>>..."+request.ToJsonString());
    //     Logger.Info(this,"Prompt request received>>>>..."+request.ToJsonString());
    //     var response = new CopilotResponse();
    //
    // }





}