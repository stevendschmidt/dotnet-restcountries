using DotNetRestCountries.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace DotNetRestCountries.Controllers
{
    [ApiController]
    public class RegionsController : ODataController
    {
        private readonly IDataService _dataService;
        public RegionsController(IDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet]
        [EnableQuery(PageSize = 25)]
        [Route("api/regions")]
        public async Task<IActionResult> GetAsync()
        {
            var regions = await _dataService.GetAllRegionsAsync();
            return Ok(regions);
        }
    }
}
