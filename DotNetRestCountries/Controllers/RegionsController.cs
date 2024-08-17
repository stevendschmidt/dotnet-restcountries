using DotNetRestCountries.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace DotNetRestCountries.Controllers
{
    [ApiController]
    public class RegionsController(IDataService dataService) : ODataController
    {
        [HttpGet]
        [EnableQuery]
        [Route("api/regions")]
        public async Task<IActionResult> GetAsync()
        {
            var regions = await dataService.GetAllRegionsAsync();
            return Ok(regions);
        }
    }
}
