using Newtonsoft.Json;

namespace Models.Weather
{
    public class Coordiantes
    {
        [JsonProperty("lat")]
        public double Latitude { get; set; }
        [JsonProperty("lon")]
        public double Longitude { get; set; }
    }
}