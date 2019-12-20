using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using TicketStore.Services;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TicketStore.Data;

namespace TicketStore.Events
{
    [Route("[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventProvider _eventProvider;
        private readonly IEmailSenderService _emailSenderService;
        private readonly ILocalDBContext _localDbContext;
        private readonly ILogger<EventsController> _logger;

        public EventsController(
            IEmailSenderService emailSenderService,
            IEventProvider eventProvider,
            ILocalDBContext localDbContext,
            ILogger<EventsController> logger)
        {
            _eventProvider = eventProvider;
            _emailSenderService = emailSenderService;
            _localDbContext = localDbContext;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return new JsonResult(await _localDbContext.Events.Include(e => e.Tickets).ToListAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong when getting events in {nameof(Get)}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Route("GetActive")]
        [HttpGet]
        public async Task<IActionResult> GetActive()
        {
            try
            {
                return new JsonResult(await _eventProvider.GetActiveEvents());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong when getting events in {nameof(Get)}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(EventToAdd newEvent)
        {
            try
            {
                await _localDbContext.Events.AddAsync(new Event
                {
                    Title = newEvent.Title,
                    Date = newEvent.Date,
                    Type = newEvent.Type,
                    Rows = newEvent.Rows,
                    Seats = newEvent.Seats
                });

                await _localDbContext.Instance.SaveChangesAsync();

                return Accepted();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong when adding an event");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Route("BuyTicket")]
        [HttpPost]
        public async Task<IActionResult> BuyTicket(TicketToBuy ticket)
        {
            try
            {
                await _localDbContext.Tickets.AddAsync(new Ticket()
                {
                    EventId = ticket.EventId,
                    Row = ticket.Row,
                    Seat = ticket.Seat,
                    Email = ticket.Email,
                    Phone = ticket.Phone
                });

                await _localDbContext.Instance.SaveChangesAsync();

                // await _emailSenderService.SendEmail(ticket.Email, $"Bought ticket for event with Id: {ticket.EventId}!");
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