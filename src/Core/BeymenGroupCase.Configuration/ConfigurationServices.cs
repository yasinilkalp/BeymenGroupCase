using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System.Runtime;

namespace BeymenGroupCase.Configuration
{
    public static class ConfigurationServices
    {
        public static void AddBeymenConfiguration(this IServiceCollection services, IConfiguration configuration, string ApplicationName)
        {
            string RedisConnectionstring = configuration.GetConnectionString("Redis");
            string RefreshTimerIntervalInMs = configuration.GetSection("RedisRefreshTimerIntervalInMs").Value;
            services.AddSingleton<IConfigurationReader>(provider =>
            {
                ConnectionMultiplexer redisConnection = ConnectionMultiplexer.Connect(RedisConnectionstring);
                var db = redisConnection.GetDatabase(db: 1);
                return new ConfigurationReader(ApplicationName, db);
            });
        }

    }
}
