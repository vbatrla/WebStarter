namespace VB.WebStarter.Helpers
{
    using System;
    using System.IO;
    using Assets;
    using Configs;
    using Newtonsoft.Json;

    public static class ConfigurationHelper
    {
        public static WebStarterConfig ReadConfig()
        {
            var filePath = $"{AppDomain.CurrentDomain.BaseDirectory}\\{WebStarterConstants.ConfigurationFilename}";
            var text = File.ReadAllText(filePath);
            var config = JsonConvert.DeserializeObject<WebStarterConfig>(text);
            return config;
        }
    }
}