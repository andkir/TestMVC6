using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace theworld50.Models
{
    public class WorldUser: IdentityUser
    {
        public DateTime FirstTrip { get; set; }
    }
}