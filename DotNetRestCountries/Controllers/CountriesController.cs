using DotNetRestCountries.Models;
using DotNetRestCountries.Models.RestCountries;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DotNetRestCountries.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountriesController : ControllerBase
    {
        private readonly ILogger<CountriesController> _logger;
        public CountriesController(ILogger<CountriesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<Country>> GetAsync()
        {
            using HttpClient client = new();
            string jsonStrResult = await client.GetStringAsync("https://restcountries.com/v3.1/all");
            IEnumerable<RcCountry> result = JsonSerializer.Deserialize<IEnumerable<RcCountry>>(jsonStrResult);
            return null;
        }
    }
}
