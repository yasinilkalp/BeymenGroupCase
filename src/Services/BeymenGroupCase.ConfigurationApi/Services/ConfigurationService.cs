using BeymenGroupCase.ConfigurationApi.Models;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace BeymenGroupCase.ConfigurationApi.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly RedisServer _redisServer;
        public ConfigurationService(RedisServer redisServer)
        {
            _redisServer = redisServer;
        }

        public async Task<bool> Add(ConfigurationModel model)
        {
            string _key = model.ApplicationName + "." + model.Name;
            return await _redisServer.Database.StringSetAsync(_key, JsonSerializer.Serialize(model));
        }

        public async Task<bool> Any(string Key)
        {
            return await _redisServer.Database.KeyExistsAsync(Key);
        }

        public async Task<bool> Delete(string Key)
        {
            if (await Any(Key))
            {
                return await _redisServer.Database.KeyDeleteAsync(Key);
            }

            return false;
        }

        public async Task<ConfigurationModel> Get(string Key)
        {
            string response = await _redisServer.Database.StringGetAsync(Key);
            return JsonSerializer.Deserialize<ConfigurationModel>(response);
        }

        public async Task<List<ConfigurationModel>> GetAll()
        {
            var executeKeys = await _redisServer.Database.ExecuteAsync("KEYS", "*");
            RedisValue[] keys = (RedisValue[]?)executeKeys;

            List<ConfigurationModel> result = new();
            foreach (var key in keys)
            {
                result.Add(await Get(key));
            } 
            return result;
        }

        public async Task<bool> Update(ConfigurationModel model)
        {
            string _key = model.ApplicationName + "." + model.Name;

            if (await Any(_key)) await Delete(_key);

            return await Add(model);
        }
    }
}
