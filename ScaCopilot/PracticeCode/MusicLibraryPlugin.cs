using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.SemanticKernel;
namespace ScaCopilot;

public class MusicLibraryPlugin
{
    [KernelFunction, 
     Description("Get a list of music recently played by the user")]
    public static string GetRecentPlays()
    {
        string dir = Directory.GetCurrentDirectory();
        string content = File.ReadAllText($"{dir}/data/recentlyplayed.txt");
        return content;
    }
    
    [KernelFunction, Description("Get a list of music available to the user")]
    public static string GetMusicLibrary()
    {
        string dir = Directory.GetCurrentDirectory();
        string content = File.ReadAllText($"{dir}/data/musiclibrary.txt");
        return content;
    }
    
    [KernelFunction, Description("Add a song to the recently played list")]
    public static string AddToRecentlyPlayed(
        [Description("The name of the artist")] string artist, 
        [Description("The title of the song")] string song, 
        [Description("The song genre")] string genre)
    {
        // Read the existing content from the file
        string filePath = "data/recentlyplayed.txt";
        string jsonContent = File.ReadAllText(filePath);
        var recentlyPlayed = (JsonArray) JsonNode.Parse(jsonContent);

        var newSong = new JsonObject
        {
            ["title"] = song,
            ["artist"] = artist,
            ["genre"] = genre
        };

        recentlyPlayed.Insert(0, newSong);
        File.WriteAllText(filePath, JsonSerializer.Serialize(recentlyPlayed,
            new JsonSerializerOptions { WriteIndented = true }));

        return $"Added '{song}' to recently played";
    }
}


public class MusicLibrarySkill
{
    public static async void Execute()
    {
        Console.WriteLine("MusicLibrarySkill content...");
        var kernel = KernelConfig.GetKernelInstance();
        kernel.ImportPluginFromType<MusicLibraryPlugin>();
        var result = await kernel.InvokeAsync<string>(
            "MusicLibraryPlugin", 
            "GetRecentPlays"
        );
        Console.WriteLine(result);
        
        var addResult = await kernel.InvokeAsync<string>(
            "MusicLibraryPlugin", 
            "AddToRecentlyPlayed", 
            new() { 
                ["artist"] = "Tiara", 
                ["song"] = "Danse", 
                ["genre"] = "French Pop, electropop, pop"
            }
        );
        Console.WriteLine(addResult);
    }

    public static async void FunctionsInPrompts()
    {
        Console.WriteLine("FunctionsInPrompts content...>><>");
        var kernel = KernelConfig.GetKernelInstance();
        kernel.ImportPluginFromType<MusicLibraryPlugin>();
        Console.WriteLine("FunctionsInPrompts ImportPluginFromType >> MusicLibraryPlugin imported");
        string prompt = @"This is a list of music available to the user:
            {{MusicLibraryPlugin.GetMusicLibrary}} 

            This is a list of music the user has recently played:
            {{MusicLibraryPlugin.GetRecentPlays}}

            Based on their recently played music, suggest a song from
            the list to play next";
        Console.WriteLine("FunctionsInPrompts prompt >> " + prompt);
        var result = await kernel.InvokePromptAsync(prompt);
        Console.WriteLine("FunctionsInPrompts result >> " + result);
        Console.WriteLine(result);
    }
}