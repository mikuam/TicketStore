using System.Collections.Generic;
using System.Threading.Tasks;

namespace TicketStore.Tickets
{
    public interface IEventProvider
    {
        Task<IEnumerable<Event>> GetActiveEvents();
    }
}