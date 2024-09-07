using System.ComponentModel;
using Microsoft.SemanticKernel;

namespace ScaCopilot.SkFunctions;

public class MenDesign
{
    private string menuText;
    
    [KernelFunction]
    [Description("Add USSD menu")]
    public  string AddMenu(string menu)
    {
       Console.WriteLine("MenuDesign.AddMenu called with menuText: " + menu);
         this.menuText = menu;
       return Guid.NewGuid().ToString();
    }

    [KernelFunction]
    [Description("Get all USSD menus.'")]
    public string GetAllMenus()
    {
        Console.WriteLine("MenuDesign.GetAllMenus called");
        return this.menuText; 
    }
    
    
    [KernelFunction]
    [Description("Update USSD menu")]
    public string UpdateMenu(string updatedMenu)
    {
        Console.WriteLine("MenuDesign.UpdateMenu called with updatedMenu: " + updatedMenu);
        this.menuText = updatedMenu;
        return "Menu updated successfully";
    }
    
}