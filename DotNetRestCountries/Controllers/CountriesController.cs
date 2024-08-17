using DotNetRestCountries.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace DotNetRestCountries.Controllers
{
    [ApiController]
    
    public class CountriesController : ODataController
    {
        private readonly IDataService _dataService;
        public CountriesController(IDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet]
        [EnableQuery(PageSize = 25)]
        [Route("api/countries")]
        public async Task<ActionResult> GetAsync()
        {
            var countries = await _dataService.GetAllCountriesAsync();
            return Ok(countries);
        }

        [HttpGet]
        [EnableQuery]
        [Route("api/countries/{code}")]
        public async Task<ActionResult> GetAsync([FromRoute] string code)
        {
            var countries = await _dataService.GetCountriesByCodeAsync(code);
            return Ok(countries);
        }
    }
}
