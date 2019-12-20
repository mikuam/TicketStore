using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace TicketStore.Data
{
    public class LocalDBContext : DbContext, ILocalDBContext
    {
        public LocalDBContext(DbContextOptions<LocalDBContext> options) : base(options)
        {   
        }

        public DbContext Instance => this;

        public DbSet<Event> Events { get; set; }

        public DbSet<Ticket> Tickets { get; set; }
    }
}
