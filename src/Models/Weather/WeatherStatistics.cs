using Newtonsoft.Json;

namespace Models.Weather
{
    public class WeatherStatistics
    {
        public int Id { get; set; }
        public string Main { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
    }
}