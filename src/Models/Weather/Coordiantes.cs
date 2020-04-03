using Newtonsoft.Json;

namespace Models.Weather
{
    public class Coordiantes
    {
        [JsonProperty("lat")]
        public long Latitude { get; set; }
        [JsonProperty("lon")]
        public long Longitude { get; set; }
    }
}