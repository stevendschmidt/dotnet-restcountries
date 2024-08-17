using System.ComponentModel.DataAnnotations;

namespace DotNetRestCountries.Models
{
    public class Region
    {
        [Key]
        public string? Name { get; set; }
        public IEnumerable<Country>? Countries { get; set; }
    }
}
