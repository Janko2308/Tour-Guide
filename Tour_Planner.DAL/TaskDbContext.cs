using Tour_Planner.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tour_Planner.DAL {
    public class TaskDbContext : DbContext {
        public DbSet<TourItem> TourItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseNpgsql("Host=localhost;Database=taskplanner;Username=postgres;Password=changeme");
        }
    }
}
