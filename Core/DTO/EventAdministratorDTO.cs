using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.DTO
{
    public class EventAdministratorDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        [MinLength(4)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(30)]
        [MinLength(4)]
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
