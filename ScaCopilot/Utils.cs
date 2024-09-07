namespace ScaCopilot;

public class Utils
{
    public static string ReadDataFile(string file)
    { 
        string fileName = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "data",file);
        Console.WriteLine("FileName is : >>> " + fileName);
        var content = System.IO.File.ReadAllText(fileName);
        return content;
    }
}