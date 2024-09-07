namespace ScaCopilot.SkFunctions;

public class MenuCreation
{
    private string _appId;
    
    public MenuCreation(string appId)
    {
        _appId = appId;
    }
    
    public string AddMenu(string menu)
    {
        Console.WriteLine("MenuCreation.AddMenu called with menuText: " + menu);
        return Guid.NewGuid().ToString();
    }
}