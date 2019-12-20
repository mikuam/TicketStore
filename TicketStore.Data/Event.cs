using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketStore.Data
{
    public class Event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime Date { get; set; }

        public EventType Type { get; set; }

        public int Rows { get; set; }

        public int Seats { get; set; }

        public List<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
