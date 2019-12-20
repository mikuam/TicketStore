using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using TicketStore.Services;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Logging;
using TicketStore.Data;
using TicketStore.Data.Repositories;

namespace TicketStore.Events
{
    [Route("[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventProvider _eventProvider;
        private readonly IEventRepository _eventRepository;
        private readonly IEmailSenderService _emailSenderService;
        private readonly ILogger<EventsController> _logger;

        public EventsController(
            IEmailSenderService emailSenderService,
            IEventProvider eventProvider,
            IEventRepository eventRepository,
            ILogger<EventsController> logger)
        {
            _eventProvider = eventProvider;
            _eventRepository = eventRepository;
            _emailSenderService = emailSenderService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return new JsonResult(await _eventRepository.GetAll());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong when getting events in {nameof(Get)}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Route("{eventId}")]
        [HttpGet]
        public async Task<IActionResult> GetById(int eventId)
        {
            try
            {
                var searchedEvent = await _eventRepository.GetWithTickets(eventId);
                if (searchedEvent == null)
                {
                    return NotFound();
                }

                return new JsonResult(searchedEvent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong when getting events in {nameof(GetById)}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(EventToAdd newEvent)
        {
            try
            {
                await _eventRepository.AddEvent(new Event
                {
                    Title = newEvent.Title,
                    Date = newEvent.Date,
                    Type = newEvent.Type,
                    Rows = newEvent.Rows,
                    Seats = newEvent.Seats
                });

                return Accepted();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong when adding an event");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Route("{eventId}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int eventId)
        {
            await _eventRepository.RemoveEvent(eventId);
            return Ok();
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

        [Route("BuyTicket")]
        [HttpPost]
        public async Task<IActionResult> BuyTicket(TicketToBuy ticket)
        {
            try
            {
                await _eventRepository.AddTicket(new Ticket()
                {
                    EventId = ticket.EventId,
                    Row = ticket.Row,
                    Seat = ticket.Seat,
                    Email = ticket.Email,
                    Phone = ticket.Phone
                });

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