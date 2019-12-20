using Microsoft.EntityFrameworkCore;

namespace TicketStore.Data
{
    public interface ILocalDBContext
    {
        public DbContext Instance { get; }

        public DbSet<Event> Events { get; set; }

        public DbSet<Ticket> Tickets { get; set; }
    }
}
