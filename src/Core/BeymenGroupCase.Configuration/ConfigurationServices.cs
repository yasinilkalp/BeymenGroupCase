using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace BeymenGroupCase.Configuration
{
    public static class ConfigurationServices
    {
        public static void AddBeymenConfiguration(this IServiceCollection services, IConfiguration configuration, string ApplicationName)
        {
            string RedisConnectionstring = configuration.GetConnectionString("Redis");
            string RefreshTimerIntervalInMs = configuration.GetSection("RedisRefreshTimerIntervalInMs").Value;

            ConnectionMultiplexer redisConnection = ConnectionMultiplexer.Connect(RedisConnectionstring);
            var db = redisConnection.GetDatabase(db: 1);

            services.AddMemoryCache();

            services.AddSingleton<IConfigurationReader>(provider =>
            {
                var cache = provider.GetRequiredService<IMemoryCache>();
                return new ConfigurationReader(ApplicationName, db, cache);
            });
        }

    }
}
