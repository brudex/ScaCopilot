using System;
using System.Collections.Generic;

namespace ScaCopilot
{
    public class SettingsData
    {
       
      
        
        public static string  EngineAppUrl = ""; 
        private static Dictionary<string, string> appSettings;
        public static string AspNetEnv = "Production";
        public static string KernelModelId = "gpt-3.5-turbo";
        public static string SkillDirectory = "skills";
        public static string ScaApiUrl = "https://sca-api.azurewebsites.net";
        public static string OpenAiApiKey { get; set; }

        public static void Initialize(Dictionary<string, string> settings)
        { 
            appSettings = settings;
            EngineAppUrl = appSettings.TryGet("EngineAppUrl"); 
            SkillDirectory = appSettings.TryGet("SkillDirectory");
            KernelModelId = appSettings.TryGet("KernelModelId");
            OpenAiApiKey = appSettings.TryGet("OpenAiApiKey");
            ScaApiUrl = appSettings.TryGet("ScaApiUrl");
        }

        public static bool IsProduction()
        {
            if (string.IsNullOrEmpty(AspNetEnv))
            {
                return false;
            }
            var isProduction = AspNetEnv.Equals("Production", StringComparison.InvariantCultureIgnoreCase);
            return isProduction;
        }
    }
}
