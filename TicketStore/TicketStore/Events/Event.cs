using System;

namespace TicketStore.Events
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
