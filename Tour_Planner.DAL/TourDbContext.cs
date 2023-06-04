using Tour_Planner.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Tour_Planner.DAL {
    public class TourDbContext : DbContext {
        public DbSet<TourItem> TourItems { get; set; }
        public DbSet<TourLogs> TourLogItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            string cp = ConfigurationManager.ConnectionStrings["PostgreSQLConnectionString"].ConnectionString;


            optionsBuilder.UseNpgsql(cp);
        }
    }
}
