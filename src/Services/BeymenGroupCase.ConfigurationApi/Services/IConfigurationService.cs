using BeymenGroupCase.ConfigurationApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeymenGroupCase.ConfigurationApi.Services
{
    public interface IConfigurationService
    {
        Task<List<ConfigurationModel>> GetAll();
        Task<bool> Add(ConfigurationModel model);
        Task<bool> Update(ConfigurationModel model);
        Task<bool> Delete(string Key);
        Task<ConfigurationModel> Get(string Key);
        Task<bool> Any(string Key);
    }
}
