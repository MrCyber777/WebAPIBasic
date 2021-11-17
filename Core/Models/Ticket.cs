using Core.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Models
{
    public  class Ticket
    {
        //[FromQuery(Name = "tId")]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        //[FromRoute(Name = "pId")]
        [Required]
        public int? ProjectId { get; set; }
        public Project? Project { get; set; }   
        public string? Owner { get; set; }

        [Ticket_FutureDateValidator]      
        [Ticket_EventDateAfterEnteredDateValidator]
        [Ticket_EventDatePresenceValidator]        
        public DateTime? EventDate { get; set; }
        [Ticket_EnteredDatePresenceValidator]
        public DateTime? EnteredDate { get; set; }
        /// <summary>
        /// When creating a ticket if EventDate is entered, it has to be in the future
        /// </summary>
        /// <returns>If valid - true, else-false</returns>
        public bool ValidateFutureDate()
        {
            if (!Id.Equals(0))
                return true;
            if (!EventDate.HasValue)
                return true;
            return (EventDate.Value > DateTime.Now);
        }
        /// <summary>
        /// When Owner is assigned, the entered date has to be present
        /// </summary>
        /// <returns>If valid - true, else-false</returns>
        public bool ValidateEnteredDatePresence()
        {
            if (string.IsNullOrWhiteSpace(Owner))
                return true;
            return EnteredDate.HasValue;
        }
        public bool ValidateEventDatePresence()
        {
            if (string.IsNullOrWhiteSpace(Owner))
                return true;
            return EventDate.HasValue;
        }
        /// <summary>
        /// If EventDate and EnteredDate dont have a value
        /// </summary>
        /// <returns>EventDate is greater or equal EnteredDate</returns>
        public bool ValidateEventDateAfterEnteredDate()
        {
            if (!EventDate.HasValue || !EnteredDate.HasValue)
                return true;
            return EventDate.Value.Date >= EnteredDate.Value.Date;
        }
    }
}
