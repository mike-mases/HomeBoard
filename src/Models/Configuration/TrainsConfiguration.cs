using System.Collections.Generic;

namespace HomeBoard.Models.Configuration
{
    public class TrainsConfiguration
    {
        public string BaseUrl { get; set; }
        public string StationId { get; set; }
        public int CacheTimeoutSeconds { get; set; }
        public List<string> Destinations { get; set; }
    }
}