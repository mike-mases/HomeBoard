using Newtonsoft.Json;

namespace Models.Weather
{
    public class Clouds
    {
        [JsonProperty("all")]
        public int CoverPercentage { get; set; }
    }
}