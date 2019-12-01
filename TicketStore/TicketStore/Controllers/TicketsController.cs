using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using TicketStore.Services;
using System.Threading.Tasks;

namespace TicketStore.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly IEmailSenderService _emailSenderService;

        public TicketsController(IEmailSenderService emailSenderService)
        {
            _emailSenderService = emailSenderService;
        }

        [Route("BuyTicket")]
        [HttpPost]
        public async Task<IActionResult> BuyTicket(Ticket ticket)
        {
            await _emailSenderService.SendEmail(ticket.Email, $"Bought ticket for {ticket.MovieTitle}!");
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}