using BeymenGroupCase.Configuration;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace BeymenGroupCase.ServiceB.Controllers
{
    [ApiController, Route("api/[controller]/[action]")]
    public class HomeController : Controller
    {

        private readonly IConfigurationReader _configurationReader;

        public HomeController(IConfigurationReader configurationReader)
        {
            _configurationReader = configurationReader;
        }

        [HttpGet("{Type}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetConfigurationValueByType(string Type)
        {
            string siteName = await _configurationReader.GetValue<string>(Type);
            return Ok(siteName);
        }
    }
}
