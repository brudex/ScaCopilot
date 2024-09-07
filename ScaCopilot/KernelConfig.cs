using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.Hosting;
using Microsoft.SemanticKernel;
 
namespace ScaCopilot
{
    public class KernelConfig
    {
        //singleton to get the kernel
        private static Kernel? _kernel;
        
        public static Kernel GetKernelInstance()
        {
            if (_kernel == null)
            {
                ConfigureKernel();
            }
            return _kernel ?? throw new System.Exception("Kernel not initialized");
        }
        


        private static void ConfigureKernel()
        {
            
            var builder = Kernel.CreateBuilder()
                .AddOpenAIChatCompletion(SettingsData.KernelModelId, SettingsData.OpenAiApiKey);
            builder.Services.AddLogging(c => c.SetMinimumLevel(LogLevel.Information).AddDebug());
            _kernel = builder.Build(); 
        } 
        
          
    }
}
 