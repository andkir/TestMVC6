using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using Microsoft.Framework.Logging;

namespace theworld50.Models
{
    public class WorldRepository : IWorldRepository
    {
        private readonly WorldContext context;
        private readonly ILogger<WorldRepository> logger;

        public WorldRepository(WorldContext context, ILogger<WorldRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public IEnumerable<Trip> GetAllTrips()
        {
            try
            {
                return context.Trips.OrderBy(t => t.Name).ToList();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return null;
            }
        }

        public IEnumerable<Trip> GetAllTripsWithStops()
        {
            try
            {
                return context.Trips
                .OrderBy(t => t.Name)
                .Include(t => t.Stops)
                .ToList();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return null;
            }
        }

        public void AddTrip(Trip newTrip)
        {
            context.Add(newTrip);
        }

        public void AddStop(string tripName, Stop newStop)
        {
            var theTrip = GetTripByName(tripName);
            newStop.Order = theTrip.Stops.Any()? theTrip.Stops.Max(s => s.Order) + 1 : 1;
            theTrip.Stops.Add(newStop);
            context.Stops.Add(newStop);
        }

        public bool SaveAll()
        {
            return context.SaveChanges() > 0;
        }

        public Trip GetTripByName(string tripName)
        {
            return context.Trips.Include(t => t.Stops).FirstOrDefault(t => t.Name == tripName);
        }
    }
}
