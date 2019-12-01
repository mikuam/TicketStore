using System.Collections.Generic;

namespace TicketStore.Tickets
{
    public interface IEventProvider
    {
        IEnumerable<Event> GetActiveEvents();
    }
}