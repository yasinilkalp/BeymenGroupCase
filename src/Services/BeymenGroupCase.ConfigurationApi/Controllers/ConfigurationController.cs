using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using System;
using BeymenGroupCase.ConfigurationApi.Services;
using BeymenGroupCase.ConfigurationApi.Models;
using System.Collections.Generic;

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
        [ProducesResponseType(typeof(List<ConfigurationModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _configurationService.GetAll());
        }

        [HttpGet("{Key}")]
        [ProducesResponseType(typeof(ConfigurationModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(string Key)
        {
            return Ok(await _configurationService.Get(Key));
        }

        [HttpDelete("{Key}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(string Key)
        {
            return Ok(await _configurationService.Delete(Key));
        }

        [HttpPost]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ConflictResult), (int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> Add(ConfigurationModel model)
        {
            string _key = model.ApplicationName + "." + model.Name;

            bool control = await _configurationService.Any(_key);
            if (control) return Conflict();

            return Ok(await _configurationService.Add(model));
        }

        [HttpPut]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update(ConfigurationModel model)
        {
            return Ok(await _configurationService.Update(model));
        }
    }
}
