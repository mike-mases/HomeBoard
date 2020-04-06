using Newtonsoft.Json;

namespace Models.Weather
{
    public class Wind
    {
        [JsonProperty("speed")]
        public double Speed { get; set; }
        [JsonProperty("deg")]
        public int Direction { get; set; }
    }
}