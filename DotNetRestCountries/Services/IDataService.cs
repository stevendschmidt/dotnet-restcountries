using DotNetRestCountries.Models;

namespace DotNetRestCountries.Services
{
    public interface IDataService
    {
        Task<IEnumerable<Country>> GetAllCountriesAsync();
        Task<IEnumerable<Country>> GetCountriesByCodeAsync(string code);
        Task<IEnumerable<Region>> GetAllRegionsAsync();
        Task<IEnumerable<Language>> GetAllLanguagesAsync();
    }
}
