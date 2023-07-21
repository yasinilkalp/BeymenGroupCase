using BeymenGroupCase.Configuration;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace BeymenGroupCase.ServiceA.Controllers
{
    [ApiController, Route("api/[controller]/[action]")]
    public class HomeController : Controller
    {

        private  readonly IConfigurationReader _configurationReader;

        public HomeController(IConfigurationReader configurationReader)
        {
            _configurationReader = configurationReader;
        }

        [HttpGet]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetConfigurationValue()
        {
            string siteName = await _configurationReader.GetValue<string>("SiteName");
            Boolean isBasketEnabled = await _configurationReader.GetValue<Boolean>("IsBasketEnabled");
            return Ok(new
            {
                siteName,
                isBasketEnabled
            });
        }
    }
}
