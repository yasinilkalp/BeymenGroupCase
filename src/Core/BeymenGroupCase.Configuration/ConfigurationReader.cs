using StackExchange.Redis;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace BeymenGroupCase.Configuration
{
    public class ConfigurationReader : IConfigurationReader
    {
        private ConfigurationSettings _settings;
        public ConfigurationReader(ConfigurationSettings settings)
        {
            _settings = settings;
        }

        public async Task<T> GetValue<T>(string key)
        {
            ConnectionMultiplexer redisConnection = ConnectionMultiplexer.Connect(_settings.ConnectionString);
            IDatabase db = redisConnection.GetDatabase(db: 1);

            string _key = _settings.ApplicationName + "." + key;
            var response = await db.StringGetAsync(_key);
            if (response.HasValue)
            {
                var configurationModel = JsonSerializer.Deserialize<ConfigurationModel>(response);
                if (configurationModel != null && configurationModel.IsActive)
                {
                    return (T)Convert.ChangeType(configurationModel.Value, typeof(T));
                }
            }
            return default;
        } 
    }
}
