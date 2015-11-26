using System.Collections.Generic;

namespace EF6Test
{
    public class SportComplex
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<SportActivity> SportActivities { get; set; }
    }
}
