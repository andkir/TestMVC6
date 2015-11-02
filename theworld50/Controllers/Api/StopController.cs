using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using AutoMapper;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Server.Kestrel;
using Microsoft.Framework.Logging;
using theworld50.Models;
using System.Threading.Tasks;
using theworld50.Services;
using theworld50.ViewModels;

namespace theworld50.Controllers.Api
{
    [Route("api/trips/{tripName}/stops")]
    public class StopController: Controller
    {
        private readonly IWorldRepository repository;
        private readonly ILogger<StopController> logger;
        private readonly CoordService coordService;

        public StopController(IWorldRepository repository, ILogger<StopController> logger, CoordService coordService)
        {
            this.repository = repository;
            this.logger = logger;
            this.coordService = coordService;
        }

        [HttpGet("")]
        public JsonResult Get(string tripName)
        {
            try
            {
                var trip = repository.GetTripByName(Uri.UnescapeDataString(tripName));

                if (trip == null)
                {
                    return Json(null);
                }

                return Json(Mapper.Map<IEnumerable<StopViewModel>>(trip.Stops));
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to get trip by name: {tripName}.", ex);
                Response.StatusCode = (int) HttpStatusCode.BadRequest;
                return Json("Error occured finding trip by name");
            }

        }

        public async Task<JsonResult> Post(string tripName, [FromBody]StopViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Map to the Entity
                    var newStop = Mapper.Map<Stop>(vm);

                    // Looking up Geocoordinates
                    var coordResult = await coordService.Lookup(newStop.Name);

                    if (!coordResult.Success)
                    {
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        Json(coordResult.Message);
                    }

                    newStop.Longitude = coordResult.Longitude;
                    newStop.Latitude = coordResult.Latitude;

                    // Save to the Database
                    repository.AddStop(tripName, newStop);

                    if (repository.SaveAll())
                    {
                        Response.StatusCode = (int)HttpStatusCode.Created;
                        return Json(Mapper.Map<StopViewModel>(newStop));
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Failed to save new stop", ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Failed to save new stop");
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json("Validation failed on new stop");

        }

    }
}