using StackExchange.Redis;
using System;
using System.Runtime;
using System.Text.Json;
using System.Threading.Tasks;

namespace BeymenGroupCase.Configuration
{
    public class ConfigurationReader : IConfigurationReader
    {
        private readonly string _applicationName;
        private readonly IDatabase _db;
        public ConfigurationReader(string applicationName, IDatabase db)
        {
            _applicationName = applicationName;
            _db = db;
        }

        public async Task<T> GetValue<T>(string key)
        { 
            string _key = _applicationName + "." + key;
            var response = await _db.StringGetAsync(new RedisKey(key));
            if (response.HasValue)
            {
                var configurationModel = JsonSerializer.Deserialize<ConfigurationModel>(response);
                if (configurationModel != null && configurationModel.IsActive)
                {
                    // Modeldeki Type alanına göre Convert ediyor. 
                    return (T)Convert.ChangeType(configurationModel.Value, configurationModel.GetType());

                    // GetValue methodu kullanılırken gönderilen T tipine göre convert eder. Ama bu sefer modeldeki Type alanının bi anlamı kalmamış olur. 
                    // return (T)Convert.ChangeType(configurationModel.Value, typeof(T));
                }
            }
            return default;
        }


    }
}
