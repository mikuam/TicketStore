using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace TicketStore.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        [Route("BuyTicket")]
        [HttpPost]
        public IActionResult BuyTicket(Ticket ticket)
        {
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}