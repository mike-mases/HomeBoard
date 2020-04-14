using System.Collections.Generic;

namespace HomeBoard.Models
{
    public class TrainsViewModel
    {
        public TrainsViewModel()
        {
            Services = new List<ServiceViewModel>();
            SpecialAnnouncements = new List<string>();
        }

        public string LastUpdated { get; set; }
        public IEnumerable<ServiceViewModel> Services { get; set; }
        public IEnumerable<string> SpecialAnnouncements { get; set; }
    }
}