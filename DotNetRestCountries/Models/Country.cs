using System.ComponentModel.DataAnnotations;

namespace DotNetRestCountries.Models
{
    public class Country
    {
        public string? CommonName { get; set; }
        public string? OfficialName { get; set; }
        public string[]? Capitals { get; set; }
        public string? Region { get; set; }

        [Key]
        public string? Cca2 { get; set; }
        public string? Ccn3 { get; set; }
        public string? Cca3 { get; set; }
        public string? Cioc {  get; set; }
        public uint? Population { get; set; }
    }
}
