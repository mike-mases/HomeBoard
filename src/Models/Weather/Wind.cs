using Newtonsoft.Json;

namespace HomeBoard.Models.Weather
{
    public class Wind
    {
        [JsonProperty("speed")]
        public double Speed { get; set; }
        [JsonProperty("deg")]
        public int Direction { get; set; }
    }
}