using Microsoft.Extensions.Caching.Memory;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace BeymenGroupCase.Configuration
{
    public class ConfigurationReader : IConfigurationReader
    {
        private readonly string _applicationName;
        private readonly IDatabase _db;
        private readonly IMemoryCache _cache;
        public ConfigurationReader(string applicationName, IDatabase db, IMemoryCache cache)
        {
            _applicationName = applicationName;
            _db = db;
            _cache = cache;
        }

        public async Task<T> GetValue<T>(string key)
        {
            string _key = _applicationName + "." + key;
            try
            {
                var response = await _db.StringGetAsync(new RedisKey(key));
                if (response.HasValue)
                {
                    var configurationModel = JsonSerializer.Deserialize<ConfigurationModel>(response);
                    if (configurationModel != null && configurationModel.IsActive)
                    {
                        // Değeri dönmeden önce başarılı isteği cache atalım.
                        CacheSetConfigurations(_key, configurationModel);

                        // Modeldeki Type alanına göre Convert ediyor. 
                        return (T)Convert.ChangeType(configurationModel.Value, configurationModel.GetType());

                        // GetValue methodu kullanılırken gönderilen T tipine göre convert eder. Ama bu sefer modeldeki Type alanının bi anlamı kalmamış olur. 
                        // return (T)Convert.ChangeType(configurationModel.Value, typeof(T));
                    }
                }
                return default;
            }
            catch (JsonException ex)
            {
                // JsonParsing için UnitTest yazıldı. Burada hatayı loglayabiliriz.  
                throw;
            }
            catch (Exception ex)
            {
                // Storage bağlanamadığı durumlarda cacheden getirelim. 
                if (ex is RedisException)
                {
                    var configuration = GetConfigurationsFromCache(_key);
                    if (configuration != null)
                        return (T)Convert.ChangeType(configuration.Value, configuration.GetType());
                }
                throw;
            }

        }

        private void CacheSetConfigurations(string key, ConfigurationModel configurations)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                // Önbellekten düşme süresi 
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
            };

            // Verileri önbelleğe ekliyoruz.
            _cache.Set(key, configurations, cacheEntryOptions);
        }

        private ConfigurationModel GetConfigurationsFromCache(string key)
        {
            // Önbellekteki verileri alıyoruz (eğer mevcutsa).
            return _cache.TryGetValue(key, out ConfigurationModel model) ? model : null;
        }

    }
}
