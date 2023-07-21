using System.Threading.Tasks;

namespace BeymenGroupCase.Configuration
{
    public interface IConfigurationReader
    {  
        Task<T> GetValue<T>(string key);
    }
}
