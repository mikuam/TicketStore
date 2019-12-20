using System;
using System.Collections.Generic;
using System.Text;

namespace TicketStore.Data.Repositories
{
    public class EventWithTicketFlat
    {
        // Event
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public EventType Type { get; set; }
        public int Rows { get; set; }
        public int Seats { get; set; }

        // Ticket
        public int TicketId { get; set; }
        public int Row { get; set; }
        public int Seat { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
