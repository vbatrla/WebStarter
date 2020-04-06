namespace VB.WebStarter.Configs
{
    using System.Collections.Generic;

    public class WebStarterConfig
    {
        public int RunTimeIntervalSeconds { get; set; }

        public List<AppPoolConfig> AppPools { get; set; }
    }
}