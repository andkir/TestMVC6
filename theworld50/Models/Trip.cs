using System;
using System.Collections.Generic;

namespace theworld50.Models
{
    public class Trip
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public DateTime Created { get; set; }
        public ICollection<Stop> Stops { get; set; }
    }
}
