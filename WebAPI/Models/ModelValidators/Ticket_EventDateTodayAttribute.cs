using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.ModelValidators
{
    public class Ticket_EventDatePastAttribute:ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            DateTime dateTime = DateTime.Now;
            if (Convert.ToDateTime(value) < dateTime)
                return new ValidationResult("The ticket can not be created for past event");
            else
                return ValidationResult.Success;
            //value = DateTime.Now.Date.AddDays(-1);
            //if (DateTime.Now.Date.CompareTo(value) > 0                                                  )
            //    return new ValidationResult("The ticket can not be created for past event");
            //else
            //    return ValidationResult.Success;

        }
    }
}
