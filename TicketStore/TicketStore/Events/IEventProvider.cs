using System.Collections.Generic;

namespace TicketStore.Events
{
    public interface IEventProvider
    {
        IEnumerable<Event> GetActiveEvents();
    }
}