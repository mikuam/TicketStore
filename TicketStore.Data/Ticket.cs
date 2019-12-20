using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketStore.Data
{
    public class Ticket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Event")]
        public int EventId { get; set; }

        public int Row { get; set; }

        public int Seat { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
    }
}
