using System.Collections.Generic;
using System.Threading.Tasks;

namespace TicketStore.Data.Repositories
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetAll();
        Task<Event> GetWithTickets(int eventId);
        Task AddEvent(Event newEvent);
        Task RemoveEvent(int eventId);
        Task AddTicket(Ticket ticket);
    }
}