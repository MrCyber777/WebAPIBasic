using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WebAPI.Models.ModelValidators;

namespace WebAPI.Models
{
    public class Ticket
    {
        //[FromQuery(Name = "tId")]
        public int Id {  get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        //[FromRoute(Name = "pId")]
        [Required]
        public int? ProjectId { get; set; }
        public string? Owner { get; set; }
        [Ticket_OwnerDate]
        [Ticket_EventDateToday]
        [Ticket_EventDatePast]
        public DateTime? EventDate {  get; set; }
    }
}
