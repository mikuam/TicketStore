using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TicketStore.Events
{
    [Route("[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var provider = new EventProvider();

                return new JsonResult(provider.GetActiveEvents());
            }
            catch (Exception)
            {
                // logging
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
