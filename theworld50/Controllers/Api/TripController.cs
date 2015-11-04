using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.Logging;
using theworld50.Models;
using theworld50.ViewModels;
using Microsoft.AspNet.Authorization;

namespace theworld50.Controllers.Api
{
    [Authorize]
    [Route("api/trips")]
    public class TripController : Controller
    {
        private readonly IWorldRepository repository;
        private readonly ILogger<TripController> logger;

        public TripController(IWorldRepository repository, ILogger<TripController> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        [HttpGet("")]
        public JsonResult Get()
        {
            return Json(Mapper.Map<IEnumerable<TripViewModel>>(repository.GetAllTripsWithStops()));
        }

        [HttpPost("")]
        public JsonResult Post([FromBody] TripViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    logger.LogInformation("Trying to save new trip");
                    var newTrip = Mapper.Map<Trip>(vm);
                    repository.AddTrip(newTrip);

                    if (repository.SaveAll())
                    {
                        Response.StatusCode = (int) HttpStatusCode.Created;
                        return Json(Mapper.Map<TripViewModel>(newTrip));
                    }
                }
            }
            catch (Exception ex)
            { 
                logger.LogError("Failed to save to database new trip", ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return Json(new {ex.Message });
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;

            return Json(new {Message = "Failed", ModelState});
        }
    }
}
