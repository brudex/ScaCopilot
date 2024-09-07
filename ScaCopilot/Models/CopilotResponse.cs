namespace ScaCopilot.Models;

public class CopilotResponse
{
    public string status { get; set; }
    public string message { get; set; }
    public List<string> frontEndActions { get; set; }
}