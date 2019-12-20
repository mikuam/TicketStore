namespace TicketStore.Data
{
    public partial class Ticket
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int Row { get; set; }
        public int Seat { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
