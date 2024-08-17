using DotNetRestCountries.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.Text.Json;

namespace DotNetRestCountries.Controllers
{
    [ApiController]
    
    public class CountriesController : ODataController
    {
        private readonly ILogger<CountriesController> _logger;
        public CountriesController(ILogger<CountriesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [EnableQuery]
        [Route("api/countries")]
        public async Task<ActionResult> GetAsync()
        {
            using HttpClient client = new();
            string jsonStrResult = await client.GetStringAsync("https://restcountries.com/v3.1/all");
            IEnumerable<RcCountry>? rcCountries = JsonSerializer.Deserialize<IEnumerable<RcCountry>>(jsonStrResult);
            var countries = rcCountries?
                .Select(x => new 
                {
                    CommonName = x.Name?.Common,
                    OfficialName = x.Name?.Official,
                    x.Region
                })
                .OrderBy(x => x.CommonName);

            return Ok(countries);
        }

        [HttpGet]
        [EnableQuery]
        [Route("api/countries/{code}")]
        public async Task<ActionResult> GetAsync([FromRoute] string code)
        {
            using HttpClient client = new();
            string jsonStrResult = await client.GetStringAsync($"https://restcountries.com/v3.1/alpha/{code}");
            IEnumerable<RcCountry>? rcCountries = JsonSerializer.Deserialize<IEnumerable<RcCountry>>(jsonStrResult);
            var countries = rcCountries?
                .Select(x => new 
                {
                    CommonName = x.Name?.Common,
                    OfficialName = x.Name?.Official,
                    x.Region
                })
                .OrderBy(x => x.CommonName);

            return Ok(countries);
        }
    }
}
