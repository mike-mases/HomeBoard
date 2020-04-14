using System.Collections.Generic;

namespace HomeBoard.Models
{
    public class ServiceViewModel
    {
        public ServiceViewModel()
        {
            CallingAt = new List<string>();
        }

        public string Time { get; set; }
        public string Destination { get; set; }
        public IEnumerable<string> CallingAt { get; set; }
        public string LastReport { get; set; }
        public string Expected { get; set; }
        public string Platform { get; set; }
        public int Coaches { get; set; }
    }
}