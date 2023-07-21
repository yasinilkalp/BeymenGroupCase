using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BeymenGroupCase.Configuration
{
    public static class ConfigurationServices
    {
        public static void AddBeymenConfiguration(this IServiceCollection services, IConfiguration configuration, string ApplicationName)
        {
            string RedisConnectionstring = configuration.GetConnectionString("Redis");
            string RefreshTimerIntervalInMs = configuration.GetSection("RedisRefreshTimerIntervalInMs").Value;
            services.AddTransient<IConfigurationReader>(provider =>
            {
                return new ConfigurationReader(new(ApplicationName, RedisConnectionstring, int.Parse(RefreshTimerIntervalInMs)));
            });
        }
    }
}
