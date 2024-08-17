using DotNetRestCountries.Models;
using System.Text.Json;

namespace DotNetRestCountries.Services
{
    public class RestCountriesV31DataService : IDataService
    {
        private const string BaseUrl = "https://restcountries.com/v3.1";
        public async Task<object> GetAllCountriesAsync()
        {
            using HttpClient client = new();
            string jsonStrResult = await client.GetStringAsync($"{BaseUrl}/all");
            IEnumerable<RcCountry>? rcCountries = JsonSerializer.Deserialize<IEnumerable<RcCountry>>(jsonStrResult);
            var countries = rcCountries?
                .Select(x => new
                {
                    CommonName = x.Name!.Common,
                    OfficialName = x.Name!.Official,
                    x.Region
                })
                .OrderBy(x => x.CommonName);
            return countries!;
        }

        public async Task<object> GetCountriesByCodeAsync(string code)
        {
            using HttpClient client = new();
            string jsonStrResult = await client.GetStringAsync($"{BaseUrl}/alpha/{code}");
            IEnumerable<RcCountry>? rcCountries = JsonSerializer.Deserialize<IEnumerable<RcCountry>>(jsonStrResult);
            var countries = rcCountries?
                .Select(x => new
                {
                    CommonName = x.Name!.Common,
                    OfficialName = x.Name!.Official,
                    x.Region
                })
                .OrderBy(x => x.CommonName);
            return countries!;
        }

        public async Task<object> GetAllRegionsAsync()
        {
            using HttpClient client = new();
            string jsonStrResult = await client.GetStringAsync($"{BaseUrl}/all");
            IEnumerable<RcCountry>? rcCountries = JsonSerializer.Deserialize<IEnumerable<RcCountry>>(jsonStrResult);
            var regions = rcCountries?
                .GroupBy(x => x.Region)
                .Select(x => new
                {
                    Name = x.Key,
                    Countries = x.Select(y => new
                    {
                        CommonName = y.Name!.Common,
                        OfficialName = y.Name!.Official,
                    })
                    .OrderBy(y => y.CommonName)
                })
                .OrderBy(x => x.Name);
            return regions!;
        }

        public async Task<object> GetAllLanguagesAsync()
        {
            using HttpClient client = new();
            string jsonStrResult = await client.GetStringAsync($"{BaseUrl}/all");
            IEnumerable<RcCountry>? rcCountries = JsonSerializer.Deserialize<IEnumerable<RcCountry>>(jsonStrResult);
            var languages = rcCountries?
                .Where(x => x.Languages != null)
                .SelectMany(x => x.Languages!.Values)
                .Distinct()
                .Select(x => new
                {
                    Name = x,
                    Countries = rcCountries
                        .Where(y => y.Languages != null)
                        .Where(y => y.Languages!.ContainsValue(x))
                        .Select(y => new 
                        {
                            CommonName = y.Name!.Common,
                            OfficialName = y.Name!.Official,
                        })
                        .OrderBy(x => x.CommonName)
                })
                .OrderBy(x => x.Name);
            return languages!;
        }
    }
}
