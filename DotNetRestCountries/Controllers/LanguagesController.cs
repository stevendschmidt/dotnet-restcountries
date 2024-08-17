using DotNetRestCountries.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace DotNetRestCountries.Controllers
{
    [ApiController]
    public class LanguagesController : ODataController
    {
        private readonly IDataService _dataService;
        public LanguagesController(IDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet]
        [EnableQuery]
        [Route("api/languages")]
        public async Task<IActionResult> GetAsync()
        {
            var languages = await _dataService.GetAllLanguagesAsync();
            return Ok(languages);
        }
    }
}
