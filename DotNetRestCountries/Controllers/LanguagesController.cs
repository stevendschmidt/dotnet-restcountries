using DotNetRestCountries.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace DotNetRestCountries.Controllers
{
    [ApiController]
    public class LanguagesController(IDataService dataService) : ODataController
    {
        [HttpGet]
        [EnableQuery]
        [Route("api/languages")]
        public async Task<IActionResult> GetAsync()
        {
            var languages = await dataService.GetAllLanguagesAsync();
            return Ok(languages);
        }
    }
}
