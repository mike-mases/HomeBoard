using System.Collections.Generic;
using Newtonsoft.Json;

namespace Models.Weather
{
    public class WeatherResponse
    {
        [JsonProperty("coord")]
        public Coordiantes Coordiantes { get; set; }
        [JsonProperty("weather")]
        public IEnumerable<WeatherStatistics> WeatherStats { get; set; }
        [JsonProperty("base")]
        public string Source { get; set; }
        [JsonProperty("main")]
        public WeatherValues Values { get; set; }
        [JsonProperty("visibility")]
        public int Visibility { get; set; }
        [JsonProperty("wind")]
        public Wind Wind { get; set; }
        public Clouds Clouds { get; set; }
        [JsonProperty("dt")]
        public int Time { get; set; }
        [JsonProperty("sys")]
        public RequestProperties RequestProps { get; set; }
        [JsonProperty("timezone")]
        public int Timezone { get; set; }
        [JsonProperty("id")]
        public int CityId { get; set; }
        [JsonProperty("name")]
        public string CityName { get; set; }
    }
}