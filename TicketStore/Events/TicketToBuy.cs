using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TicketStore.Events
{
    public class TicketToBuy : IValidatableObject
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int EventId { get; set; }

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
            if (Row > 1000)
            {
                yield return new ValidationResult("Row cannot be greater then 1000");
            }

            if (Seat > 1000)
            {
                yield return new ValidationResult("Row cannot be greater then 1000");
            }
        }
    }
}
