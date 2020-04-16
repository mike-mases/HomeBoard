namespace HomeBoard.Models
{
    public class HomeBoardViewModel
    {
        public HomeBoardViewModel()
        {
            Trains = new TrainsViewModel();
            Weather = new WeatherViewModel();
        }
        
        public TrainsViewModel Trains { get; set; }
        public WeatherViewModel Weather { get; set; }
    }
}