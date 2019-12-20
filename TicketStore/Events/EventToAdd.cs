using System;
using System.ComponentModel.DataAnnotations;
using TicketStore.Data;

namespace TicketStore.Events
{
    public class EventToAdd
    {
        [Title]
        [MinLength(1)]
        public string Title { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public EventType Type { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Rows { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Seats { get; set; }
    }
}
