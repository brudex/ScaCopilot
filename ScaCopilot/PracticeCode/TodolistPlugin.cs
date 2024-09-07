namespace ScaCopilot;

using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.SemanticKernel;

public class TodoListPlugin
{
    [KernelFunction, Description("Mark a todo list item as complete")]
    public static string CompleteTask([Description("The task to complete")] string task)
    {
        // Read the JSON file
        string jsonFilePath = $"{Directory.GetCurrentDirectory()}/todo.txt";
        string jsonContent = File.ReadAllText(jsonFilePath);

        // Parse the JSON content
        JsonNode todoData = JsonNode.Parse(jsonContent);

        // Find the task and mark it as complete
        JsonArray todoList = (JsonArray) todoData["todoList"];
        foreach (JsonNode taskNode in todoList)
        {
            if (taskNode["task"].ToString() == task)
            {
                taskNode["completed"] = true;
                break;
            }
        }

        // Save the modified JSON back to the file
        File.WriteAllText(jsonFilePath, JsonSerializer.Serialize(todoData));
        return $"Task '{task}' marked as complete.";
    }
}



public class TodoListSkill
{
    public static async  void Execute()
    {
        // Translate the content
        Console.WriteLine("Translating content...");
        var kernel = KernelConfig.GetKernelInstance();
        kernel.ImportPluginFromType<TodoListPlugin>();
        var result = await kernel.InvokeAsync<string>(
            "TodoListPlugin", 
            "CompleteTask", 
            new() {{ "task", "Buy groceries" }}
        );
        Console.WriteLine(result);
        
        
         
        Console.WriteLine($"TodoListSkill content: {result}"); 
    }
}