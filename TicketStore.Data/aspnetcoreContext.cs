using Microsoft.EntityFrameworkCore;

namespace TicketStore.Data
{
    public partial class aspnetcoreContext : DbContext, ILocalDBContext
    {
        public aspnetcoreContext()
        {
        }

        public aspnetcoreContext(DbContextOptions<aspnetcoreContext> options)
            : base(options)
        {
        }

        public DbContext Instance => this;

        public DbSet<Event> Events { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Phone).HasMaxLength(20);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
