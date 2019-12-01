using System.ComponentModel.DataAnnotations;

namespace TicketStore
{
    public class Ticket
    {
        [MinLength(1)]
        public string MovieTitle { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Row { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Seat { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string Phone { get; set; }
    }
}
