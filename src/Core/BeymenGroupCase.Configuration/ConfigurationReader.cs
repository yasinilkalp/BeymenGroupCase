using System.Threading.Tasks;

namespace BeymenGroupCase.Configuration
{
    public class ConfigurationReader : IConfigurationReader
    {
        public async Task<T> GetValue<T>(string key)
        { 
           return default;
        }
    }
}
