using Newtonsoft.Json;

namespace HomeBoard.Models.Weather
{
    public class WeatherValues
    {
        [JsonProperty("temp")]
        public double Temperature { get; set; }
        [JsonProperty("feels_like")]
        public double FeelsLike { get; set; }
        [JsonProperty("temp_min")]
        public double MinTemp { get; set; }
        [JsonProperty("temp_max")]
        public double MaxTemp { get; set; }
        [JsonProperty("pressure")]
        public int Pressure { get; set; }
        [JsonProperty("humidity")]
        public int Humidity { get; set; }
    }
}