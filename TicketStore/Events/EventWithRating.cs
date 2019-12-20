using TicketStore.Data;

namespace TicketStore.Events
{
    public class EventWithRating : Event
    {
        public EventWithRating(Event ev)
        {
            Id = ev.Id;
            Title = ev.Title;
            Type = ev.Type;
            Date = ev.Date;
            Rows = ev.Rows;
            Seats = ev.Seats;
        }

        public decimal Rating { get; set; }
    }
}
