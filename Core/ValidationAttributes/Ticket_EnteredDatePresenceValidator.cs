using Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.ValidationAttributes
{
    public class Ticket_EnteredDatePresenceValidator: ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var ticket = validationContext.ObjectInstance as Ticket;
            if (ticket is null) 
                return new ValidationResult("Ticket can not be null");
            if(!ticket.ValidateEnteredDatePresence())
                return new ValidationResult(" EnteredDate has to be present");
            return ValidationResult.Success;

        }
    }
}
