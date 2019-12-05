using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using TicketStore.Services;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Logging;

namespace TicketStore.Tickets
{
    [Route("[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly IEventProvider _eventProvider;
        private readonly IEmailSenderService _emailSenderService;
        private readonly ILogger<TicketsController> _logger;

        public TicketsController(
            IEmailSenderService emailSenderService,
            IEventProvider eventProvider,
            ILogger<TicketsController> logger)
        {
            _eventProvider = eventProvider;
            _emailSenderService = emailSenderService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAvailableEvents()
        {
            try
            {
                return new JsonResult(await _eventProvider.GetActiveEvents());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong when getting events in {nameof(GetAvailableEvents)}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Route("BuyTicket")]
        [HttpPost]
        public async Task<IActionResult> BuyTicket(Ticket ticket)
        {
            try
            {
                await _emailSenderService.SendEmail(ticket.Email, $"Bought ticket for {ticket.MovieTitle}!");
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong when buying tickets in {nameof(BuyTicket)}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}