using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.ModelValidators
{
    public class Ticket_EventDateTodayAttribute:ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            value = DateTime.Now.Date;
            if (DateTime.Now.Date.CompareTo(value) == 0)
                return new ValidationResult("The ticket can not be created for today");
            else
                return ValidationResult.Success;

        }
    }
}
