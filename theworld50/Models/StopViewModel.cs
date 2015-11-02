using System;

namespace theworld50.Models
{
    public class StopViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public DateTime Arrived { get; set; }
    }
}