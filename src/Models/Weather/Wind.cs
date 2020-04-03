using Newtonsoft.Json;

namespace Models.Weather
{
    public class Wind
    {
        [JsonProperty("speed")]
        public long Speed { get; set; }
        [JsonProperty("deg")]
        public int Direction { get; set; }
    }
}