using System;

namespace BeymenGroupCase.Configuration
{
    public class ConfigurationSettings
    {
        public ConfigurationSettings()
        {

        }

        public ConfigurationSettings(string applicationName, string connectionString, int refreshTimerIntervalInMs)
        {
            ApplicationName = applicationName;
            ConnectionString = connectionString;
            RefreshTimerIntervalInMs = refreshTimerIntervalInMs;
        }

        public string ApplicationName { get; set; }
        public string ConnectionString { get; set; }
        public int RefreshTimerIntervalInMs { get; set; }
    }
}
