using Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.ValidationAttributes
{
    public class Ticket_FutureDateValidator:ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var ticket = validationContext.ObjectInstance as Ticket;
            if (ticket is null)
                return new ValidationResult("Ticket can not be null");
            if (!ticket.ValidateFutureDate())
                return new ValidationResult(" EventDate has to be in the future");
            return ValidationResult.Success;    
        }
    }
}
