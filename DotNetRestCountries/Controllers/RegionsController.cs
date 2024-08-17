using DotNetRestCountries.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.Text.Json;

namespace DotNetRestCountries.Controllers
{
    [ApiController]
    public class RegionsController : ODataController
    {
        private readonly ILogger<RegionsController> _logger;
        public RegionsController(ILogger<RegionsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [EnableQuery]
        [Route("api/regions")]
        public async Task<ActionResult> GetAsync()
        {
            using HttpClient client = new();
            string jsonStrResult = await client.GetStringAsync("https://restcountries.com/v3.1/all");
            IEnumerable<RcCountry>? rcCountries = JsonSerializer.Deserialize<IEnumerable<RcCountry>>(jsonStrResult);
            var regions = rcCountries?
                .GroupBy(x => x.Region)
                .Select(x => new 
                {
                    Name = x.Key,
                    Countries = x.Select(y => new
                    {
                        CommonName = y.Name?.Common,
                        OfficialName = y.Name?.Official,
                    })
                    .OrderBy(y => y.CommonName)
                })
                .OrderBy(x => x.Name);

            return Ok(regions);
        }
    }
}
