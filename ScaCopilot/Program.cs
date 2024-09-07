using ScaCopilot.SkFunctions;
namespace ScaCopilot;


internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        // Add services to the container. 
        builder.Services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.AddLog4Net("log4net.config");
        });
        Logger.EnsureInitialized();
        builder.Services.AddControllers(); 
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        var app = builder.Build();
        //get connection string
        var settings = builder.Configuration.GetSection("SettingsData").GetChildren()
            .Select(item => new KeyValuePair<string, string>(item.Key, item.Value)).ToDictionary(x => x.Key, x => x.Value); 
        SettingsData.Initialize(settings);
        FunctionList.Load();
        Console.WriteLine("SettingsData Initialized");
        //TranslateContentSkill.Execute();
        //PluginFunctionCalling.Execute();
        //PromptTemplates.Prompt3();
        //MusicLibrarySkill.FunctionsInPrompts();
        PromptServiceTest.Execute();
        //Console.WriteLine("TranslateContentSkill executed");
        //Console.ReadLine();
        // Configure the HTTP request pipeline.
        // if (app.Environment.IsDevelopment())
        // {
        //     app.UseSwagger();
        //     app.UseSwaggerUI();
        //     app.UseHttpLogging();
        // }
        // app.UseAuthorization();
        // app.MapControllers();
        // app.Run();
    }
}