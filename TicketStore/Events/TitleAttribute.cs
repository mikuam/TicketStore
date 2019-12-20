using System;
using System.ComponentModel.DataAnnotations;

namespace TicketStore.Events
{
    public class TitleAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var ticket = (EventToAdd)validationContext.ObjectInstance;

            if (ticket.Title.Contains("Lords of the rings"))
            {
                return new ValidationResult("Sorry, movie not available.");
            }

            return ValidationResult.Success;
        }
    }
}
