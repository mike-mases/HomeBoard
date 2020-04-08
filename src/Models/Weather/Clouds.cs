using Newtonsoft.Json;

namespace HomeBoard.Models.Weather
{
    public class Clouds
    {
        [JsonProperty("all")]
        public int CoverPercentage { get; set; }
    }
}