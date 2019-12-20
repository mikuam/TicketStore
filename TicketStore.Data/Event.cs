using System;
using System.Collections.Generic;

namespace TicketStore.Data
{
    public partial class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public EventType Type { get; set; }
        public int Rows { get; set; }
        public int Seats { get; set; }

        public List<Ticket> Tickets { get; set; }
    }
}
