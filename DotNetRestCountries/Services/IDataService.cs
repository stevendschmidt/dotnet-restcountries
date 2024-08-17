namespace DotNetRestCountries.Services
{
    public interface IDataService
    {
        Task<object> GetAllCountriesAsync();
        Task<object> GetCountriesByCodeAsync(string code);
        Task<object> GetAllRegionsAsync();
        Task<object> GetAllLanguagesAsync();
    }
}
