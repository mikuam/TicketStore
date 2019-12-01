using System;
using System.ComponentModel.DataAnnotations;

namespace TicketStore.Tickets
{
    public class TitleAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var ticket = (Ticket)validationContext.ObjectInstance;

            if (ticket.MovieTitle.Contains("Lords of the rings"))
            {
                return new ValidationResult("Sorry, movie not available.");
            }

            return ValidationResult.Success;
        }
    }
}
