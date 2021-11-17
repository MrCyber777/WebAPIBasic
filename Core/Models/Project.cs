using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Models
{
    public class Project
    {      
        public int Id { get; set; }
        [Required]
        [StringLength(100)]       
        public string Name { get; set; }       
        public List<Ticket>? Tickets { get; set; }
    }
}
