using DotNetRestCountries.Models;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace DotNetRestCountries.Services
{
    public class RestCountriesV31DataService(IHttpClientFactory httpClientFactory, IMemoryCache memoryCache) : IDataService
    {
        private const string BaseUrl = "https://restcountries.com/v3.1";

        public async Task<IEnumerable<Country>> GetAllCountriesAsync()
        {
            string request = "/all?fields=name,capital,region,cca2,ccn3,cca3,cioc,population,languages";
            IEnumerable<RcCountry>? rcCountries = await GetCachedCountriesAsync(request);
            IEnumerable<Country>? countries = rcCountries?
                .Select(x => new Country()
                {
                    CommonName = x.Name!.Common,
                    OfficialName = x.Name!.Official,
                    Capitals = x.Capital,
                    Region = x.Region,
                    Cca2 = x.Cca2,
                    Ccn3 = x.Ccn3,
                    Cca3 = x.Cca3,
                    Cioc = x.Cioc,
                    Population = x.Population
                })
                .OrderBy(x => x.CommonName);
            return countries!;
        }

        public async Task<IEnumerable<Country>> GetCountriesByCodeAsync(string code)
        {
            string request = $"/alpha/{code}";
            IEnumerable<RcCountry>? rcCountries = await GetCachedCountriesAsync(request);
            IEnumerable<Country>? countries = rcCountries?
                .Select(x => new Country()
                {
                    CommonName = x.Name!.Common,
                    OfficialName = x.Name!.Official,
                    Capitals = x.Capital,
                    Region = x.Region,
                    Cca2 = x.Cca2,
                    Ccn3 = x.Ccn3,
                    Cca3 = x.Cca3,
                    Cioc = x.Cioc,
                    Population = x.Population
                })
                .OrderBy(x => x.CommonName);
            return countries!;
        }

        public async Task<IEnumerable<Region>> GetAllRegionsAsync()
        {
            string request = "/all?fields=name,capital,region,cca2,ccn3,cca3,cioc,population,languages";
            IEnumerable<RcCountry>? rcCountries = await GetCachedCountriesAsync(request);
            IEnumerable<Region>? regions = rcCountries?
                .GroupBy(x => x.Region)
                .Select(x => new Region()
                {
                    Name = x.Key,
                    Countries = x.Select(y => new Country()
                    {
                        CommonName = y.Name!.Common,
                        OfficialName = y.Name!.Official,
                        Capitals = y.Capital,
                        Region = y.Region,
                        Cca2 = y.Cca2,
                        Ccn3 = y.Ccn3,
                        Cca3 = y.Cca3,
                        Cioc = y.Cioc,
                        Population = y.Population
                    })
                    .OrderBy(y => y.CommonName)
                })
                .OrderBy(x => x.Name);
            return regions!;
        }

        public async Task<IEnumerable<Language>> GetAllLanguagesAsync()
        {
            string request = "/all?fields=name,capital,region,cca2,ccn3,cca3,cioc,population,languages";
            IEnumerable<RcCountry>? rcCountries = await GetCachedCountriesAsync(request);
            IEnumerable<Language>? languages = rcCountries?
                .Where(x => x.Languages != null)
                .SelectMany(x => x.Languages!.Values)
                .Distinct()
                .Select(x => new Language()
                {
                    Name = x,
                    Countries = rcCountries
                        .Where(y => y.Languages != null)
                        .Where(y => y.Languages!.ContainsValue(x))
                        .Select(y => new Country()
                        {
                            CommonName = y.Name!.Common,
                            OfficialName = y.Name!.Official,
                            Capitals = y.Capital,
                            Region = y.Region,
                            Cca2 = y.Cca2,
                            Ccn3 = y.Ccn3,
                            Cca3 = y.Cca3,
                            Cioc = y.Cioc,
                            Population = y.Population
                        })
                        .OrderBy(x => x.CommonName)
                })
                .OrderBy(x => x.Name);
            return languages!;
        }

        private async Task<IEnumerable<RcCountry>> GetCachedCountriesAsync(string request)
        {
            using HttpClient client = httpClientFactory.CreateClient();

            string? jsonStrResult = await memoryCache.GetOrCreateAsync(
                request,
                cacheEntry =>
                {
                    cacheEntry.SlidingExpiration = TimeSpan.FromSeconds(3);
                    cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(20);
                    return client.GetStringAsync($"{BaseUrl}{request}");
                });
            IEnumerable<RcCountry>? rcCountries = JsonSerializer.Deserialize<IEnumerable<RcCountry>>(jsonStrResult!);

            return rcCountries!;
        }
    }
}
