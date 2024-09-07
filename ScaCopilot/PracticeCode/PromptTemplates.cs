using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

namespace ScaCopilot;

public class PromptTemplates
{
    public static async void Execute()
    {
        Console.WriteLine("PluginFunctionCalling Translating content...");
        var kernel = KernelConfig.GetKernelInstance();
        kernel.ImportPluginFromType<ConversationSummaryPlugin>();
        string history = @"In the heart of my bustling kitchen, I have embraced 
    the challenge of satisfying my family's diverse taste buds and 
    navigating their unique tastes. With a mix of picky eaters and 
    allergies, my culinary journey revolves around exploring a plethora 
    of vegetarian recipes.

    One of my kids is a picky eater with an aversion to anything green, 
    while another has a peanut allergy that adds an extra layer of complexity 
    to meal planning. Armed with creativity and a passion for wholesome 
    cooking, I've embarked on a flavorful adventure, discovering plant-based 
    dishes that not only please the picky palates but are also heathy and 
    delicious.";

        string prompt = @"This is some information about the user's background: 
    {{$history}}

    Given this user's background, provide a list of relevant recipes.";

        var result = await kernel.InvokePromptAsync(prompt, 
            new KernelArguments() {{ "history", history }});

        Console.WriteLine(result);
    }


    public static async void Prompt2()
    {
        Console.WriteLine("PluginFunctionCalling Translating content...");
        var kernel = KernelConfig.GetKernelInstance();
        string language = "French";
        string history = @"I'm traveling with my kids and one of them has a peanut allergy.";

        string prompt = @$"
    You are a travel assistant. You are helpful, creative, and very friendly. 
    Consider the traveler's background:
    ${history}

    Create a list of helpful phrases and words in ${language} a traveler would find useful.

    Group phrases by category. Include common direction words. 
    Display the phrases in the following format: 
    Hello - Ciao [chow]

    Begin with: 'Here are some phrases in ${language} you may find helpful:' 
    and end with: 'I hope this helps you on your trip!'";
        var result = await kernel.InvokePromptAsync(prompt);
        Console.WriteLine(result);
    }
    
    
    public static async void Prompt3()
    {
        Console.WriteLine("Prompt with example for the LLM instructions  ...");
        var kernel = KernelConfig.GetKernelInstance();
      
        string input = @"I have a vacation from June 1 to July 22. I want to go to Greece. 
            I live in Chicago."; 
                string prompt = @$"
        <message role=""system"">Instructions: Identify the from and to destinations 
        and dates from the user's request</message>
        <message role=""user"">Can you give me a list of flights from Seattle to Tokyo? 
        I want to travel from March 11 to March 18.</message>
        <message role=""assistant"">Seattle|Tokyo|03/11/2024|03/18/2024</message>
        <message role=""user"">${input}</message>";
        var result = await kernel.InvokePromptAsync(prompt);
        Console.WriteLine("Result :   "+result);
    }



    public static async void Prompt4()
    {
        Console.WriteLine("Prompt with example for the LLM instructions  ...");
        var kernel = KernelConfig.GetKernelInstance();

        kernel.ImportPluginFromType<ConversationSummaryPlugin>();
        var prompts = kernel.ImportPluginFromPromptDirectory("skills/TravelPlugins");

        ChatHistory history = new();
        string input = @"I'm planning an anniversary trip with my spouse. We like hiking, 
    mountains, and beaches. Our travel budget is $15000";

        var result = await kernel.InvokeAsync<string>(prompts["SuggestDestinations"],
            new() { { "input", input } });

        Console.WriteLine(result);
        history.AddUserMessage(input);
        history.AddAssistantMessage(result);

        Console.WriteLine("Where would you like to go?");
        input = Console.ReadLine();

        result = await kernel.InvokeAsync<string>(prompts["SuggestActivities"],
            new()
            {
                { "history", history },
                { "destination", input },
            }
        );
        Console.WriteLine(result);
    }


    public static async void FunctionsInPrompts()
    {
        var kernel = KernelConfig.GetKernelInstance();

         string history = @"In the heart of my bustling kitchen, I have embraced the challenge 
    of satisfying my family's diverse taste buds and navigating their unique tastes. 
    With a mix of picky eaters and allergies, my culinary journey revolves around 
    exploring a plethora of vegetarian recipes.

    One of my kids is a picky eater with an aversion to anything green, while another 
    has a peanut allergy that adds an extra layer of complexity to meal planning. 
    Armed with creativity and a passion for wholesome cooking, I've embarked on a 
    flavorful adventure, discovering plant-based dishes that not only please the 
    picky palates but are also heathy and delicious.";
        
        kernel.ImportPluginFromType<ConversationSummaryPlugin>();

        string prompt = @"User information: 
    {{ConversationSummaryPlugin.SummarizeConversation $history}}

    Given this user's background information, provide a list of relevant recipes.";

        var result = await kernel.InvokePromptAsync<string>(prompt, new(){ { "history", history }});
        Console.WriteLine(result);
    }
}