using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TicketStore.Events
{
    [Route("[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventProvider _eventProvider;

        public EventsController(IEventProvider eventProvider)
        {
            _eventProvider = eventProvider;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return new JsonResult(_eventProvider.GetActiveEvents());
            }
            catch (Exception)
            {
                // logging
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
