using System;

namespace theworld50.Models
{
    public class Stop
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public DateTime Arrived { get; set; }
        public int Order { get; set; }
    }
}