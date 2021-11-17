using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStore.EF
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Project>Projects {  get; set; }
        public DbSet<Ticket>Tickets {  get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                 .HasMany(x => x.Tickets)
                 .WithOne(x => x.Project)
                 .HasForeignKey(x => x.ProjectId);

            modelBuilder.Entity<Project>().HasData(
                new Project { Id = 1, Name = "Project 1" },
                new Project { Id = 2, Name = "Project 2" },
                new Project { Id = 3, Name = "Project 3" }
                );
            modelBuilder.Entity<Ticket>().HasData(
                new Ticket { Id = 1, Title = "Ticket 1", ProjectId = 1, Description = "Ticket For Project 1" },
                new Ticket { Id = 2, Title = "Ticket 2", ProjectId = 2, Description = "Ticket For Project 2" },
                new Ticket { Id = 3, Title = "Ticket 3", ProjectId = 3, Description = "Ticket For Project 3" }
                );
        }

    }
}
