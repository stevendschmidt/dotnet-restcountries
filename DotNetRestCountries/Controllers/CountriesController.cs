using DotNetRestCountries.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace DotNetRestCountries.Controllers
{
    [ApiController]
    public class CountriesController(IDataService dataService) : ODataController
    {
        /// <summary>
        /// Gets a list of countries
        /// </summary>
        /// <remarks>
        /// OData system query options are supported. Sample requests:
        ///
        ///     GET /countries
        ///     GET /countries?$select=commonName,officialName
        ///     GET /countries?$filter=contains(commonName,'United')
        ///     GET /countries?$orderby=population desc
        ///
        /// Page size is __25__; use ```@odata.nextLink``` to continue.
        /// </remarks>
        /// <response code="200">Returns the list of countries and their properties</response>
        [HttpGet]
        [EnableQuery(PageSize = 25)]
        [Route("api/countries")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync()
        {
            var countries = await dataService.GetAllCountriesAsync();
            return Ok(countries);
        }

        /// <summary>
        /// Gets a specific country's details by its country code
        /// </summary>
        /// <remarks>
        /// OData system query options are supported. Sample requests:
        ///
        ///     GET /countries/usa
        ///     GET /countries/rsa?$select=commonName,capitals
        /// </remarks>
        /// <response code="200">Returns the specific country and its properties</response>
        [HttpGet]
        [EnableQuery]
        [Route("api/countries/{code}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync([FromRoute] string code)
        {
            var countries = await dataService.GetCountriesByCodeAsync(code);
            return Ok(countries);
        }
    }
}
