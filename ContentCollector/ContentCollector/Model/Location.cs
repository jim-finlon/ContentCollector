using System;
using Newtonsoft.Json;

namespace ContentCollector.Model
{
    public class Location
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Altitude { get; set; }
        public string Geolocation { get; set; }

    }
}
