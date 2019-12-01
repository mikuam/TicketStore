using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TicketStore
{
    public class Ticket : IValidatableObject
    {
        [Title]
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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Row > 30)
            {
                yield return new ValidationResult("Row cannot be greater then 30");
            }

            if (Seat > 20)
            {
                yield return new ValidationResult("Row cannot be greater then 20");
            }
        }
    }
}
