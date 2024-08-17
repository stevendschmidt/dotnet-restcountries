using Microsoft.OData.ModelBuilder;
using System.ComponentModel.DataAnnotations;

namespace DotNetRestCountries.Models
{
    public class Language
    {
        [Key]
        public string? Name { get; set; }

        [AutoExpand]
        public IEnumerable<Country>? Countries { get; set; }
    }
}
