using DotNetRestCountries.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace DotNetRestCountries.Controllers
{
    [ApiController]
    public class LanguagesController(IDataService dataService) : ODataController
    {
        /// <summary>
        /// Gets a list of all languages and the countries that speak them
        /// </summary>
        /// <remarks>
        /// OData system query options are supported. Sample requests:
        ///
        ///     GET /languages
        ///     GET /languages?$filter=name eq 'French'
        /// </remarks>
        /// <response code="200">Returns the list of all languages and the countries that speak them</response>
        [HttpGet]
        [EnableQuery]
        [Route("api/languages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync()
        {
            var languages = await dataService.GetAllLanguagesAsync();
            return Ok(languages);
        }
    }
}
