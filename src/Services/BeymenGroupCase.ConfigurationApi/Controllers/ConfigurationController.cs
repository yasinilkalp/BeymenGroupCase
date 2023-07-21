using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using System;
using BeymenGroupCase.ConfigurationApi.Services;
using BeymenGroupCase.ConfigurationApi.Models;

namespace BeymenGroupCase.ConfigurationApi.Controllers
{
    [ApiController, Route("api/[controller]/[action]")]
    public class ConfigurationController : Controller
    {
        private readonly IConfigurationService _configurationService; 
        public ConfigurationController(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _configurationService.GetAll());
        }

        [HttpGet("{Key}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(string Key)
        {
            return Ok(await _configurationService.Get(Key));
        }

        [HttpDelete("{Key}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(string Key)
        {
            return Ok(await _configurationService.Delete(Key));
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(ConfigurationModel model)
        {
            return Ok(await _configurationService.Add(model));
        }

        [HttpPut]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update(ConfigurationModel model)
        {
            return Ok(await _configurationService.Update(model));
        }
    }
}
