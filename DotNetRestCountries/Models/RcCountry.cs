using System.Text.Json.Serialization;

namespace DotNetRestCountries.Models
{
    public class RcCountry
    {
        [JsonPropertyName("name")]
        public Name? Name { get; set; }

        [JsonPropertyName("region")]
        public string? Region { get; set; }
    }

    public class Name
    {
        [JsonPropertyName("common")]
        public string? Common { get; set; }

        [JsonPropertyName("official")]
        public string? Official { get; set; }
    }
}