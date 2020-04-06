namespace VB.WebStarter.Assets
{
    public class WebStarterConstants
    {
        public const string ConfigurationFilename = "WebStarterConfig.json";


        public const string ServiceName = "WebStarter";

        public const string ServiceDescription =
            "This service is responsible for recycling appPools that are set in configuration f" +
            "ile. Based on state of the appPool it could be recycled or started if the sql query r" +
            "esult returns zero rows.";
    }
}