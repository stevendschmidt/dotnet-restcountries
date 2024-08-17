using DotNetRestCountries.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace DotNetRestCountries.Controllers
{
    [ApiController]
    public class RegionsController(IDataService dataService) : ODataController
    {
        /// <summary>
        /// Gets a list of all regions and the countries within them
        /// </summary>
        /// <remarks>
        /// OData system query options are supported. Sample requests:
        ///
        ///     GET /regions
        ///     GET /regions?$filter=name eq 'Americas'
        /// </remarks>
        /// <response code="200">Returns the list of all regions and their countries</response>
        [HttpGet]
        [EnableQuery]
        [Route("api/regions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync()
        {
            var regions = await dataService.GetAllRegionsAsync();
            return Ok(regions);
        }
    }
}
