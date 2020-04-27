namespace HomeBoard.Models
{
    public class WeatherViewModel
    {
        public string LastUpdated { get; set; }
        public double Temperature { get; set; }
        public double FeelsLike { get; set; }
        public double MinTemp { get; set; }
        public double MaxTemp { get; set; }
        public string CityName { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
    }
}