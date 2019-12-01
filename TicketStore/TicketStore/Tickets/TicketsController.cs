using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using TicketStore.Services;
using System.Threading.Tasks;
using System;

namespace TicketStore.Tickets
{
    [Route("[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly IEventProvider _eventProvider;
        private readonly IEmailSenderService _emailSenderService;

        public TicketsController(IEmailSenderService emailSenderService, IEventProvider eventProvider)
        {
            _eventProvider = eventProvider;
            _emailSenderService = emailSenderService;
        }

        [HttpGet]
        public IActionResult GetAvailableEvents()
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

        [Route("BuyTicket")]
        [HttpPost]
        public async Task<IActionResult> BuyTicket(Ticket ticket)
        {
            await _emailSenderService.SendEmail(ticket.Email, $"Bought ticket for {ticket.MovieTitle}!");
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}