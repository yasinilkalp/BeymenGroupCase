using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace BeymenGroupCase.ConfigurationApi.Services
{
    public class RedisServer
    {
        private ConnectionMultiplexer _connectionMultiplexer;
        private IDatabase _database;
        private string configurationString;
        private ConfigurationOptions configurationOptions;


        public RedisServer(IConfiguration configuration)
        {
            CreateRedisConfigurationString(configuration);

            _connectionMultiplexer = ConnectionMultiplexer.Connect(configurationOptions);
            _database = _connectionMultiplexer.GetDatabase(configurationOptions.DefaultDatabase.Value);
        }

        public IDatabase Database => _database;

        public void FlushDatabase()
        {
            _connectionMultiplexer.GetServer(configurationString).FlushDatabase(configurationOptions.DefaultDatabase.Value);
        }
         

        private void CreateRedisConfigurationString(IConfiguration configuration)
        { 
            configurationString = configuration.GetConnectionString("Redis"); 
            configurationOptions = new ConfigurationOptions
            {
                EndPoints = { configurationString },
                AbortOnConnectFail = false,
                DefaultDatabase = 1,
                AllowAdmin = true,
                ClientName = "BeymenConfiguration"
            };
        }
    }
}
