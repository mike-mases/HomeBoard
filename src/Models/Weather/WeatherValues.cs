using Newtonsoft.Json;

namespace Models.Weather
{
    public class WeatherValues
    {
        [JsonProperty("temp")]
        public long Temperature { get; set; }
        [JsonProperty("feels_like")]
        public long FeelsLike { get; set; }
        [JsonProperty("temp_min")]
        public long MinTemp { get; set; }
        [JsonProperty("temp_max")]
        public long MaxTemp { get; set; }
        [JsonProperty("pressure")]
        public int Pressure { get; set; }
        [JsonProperty("humidity")]
        public int Humidity { get; set; }
    }
}