using System.Collections.Generic;
using System.Threading.Tasks;

namespace TicketStore.Events
{
    public interface IEventProvider
    {
        Task<IEnumerable<EventWithRating>> GetActiveEvents();
    }
}