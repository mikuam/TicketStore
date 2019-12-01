using System;

namespace TicketStore.Tickets
{
    public class Event
    {
        public int EventId { get; set; }

        public string Title { get; set; }

        public DateTime Date { get; set; }

        public EventType Type { get; set; }

        public decimal Rating { get; set; }
    }
}
