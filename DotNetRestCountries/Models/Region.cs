using Microsoft.OData.ModelBuilder;
using System.ComponentModel.DataAnnotations;

namespace DotNetRestCountries.Models
{
    public class Region
    {
        [Key]
        public string? Name { get; set; }

        [AutoExpand]
        public IEnumerable<Country>? Countries { get; set; }
    }
}
