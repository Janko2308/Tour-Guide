using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tour_Planner.Model;

namespace Tour_Planner.DAL {
    public class DataManagerEntityFrameworkImpl : IDataManager {
        private readonly TourDbContext context = new TourDbContext();

        public DataManagerEntityFrameworkImpl() {
            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
        
        public void AddTour(TourItem t) {
            context.TourItems.Add(t);
            context.SaveChanges();
        }

        public IEnumerable<TourItem> GetTours() {
            context.TourItems.Load();
            return context.TourItems;
        }
    }
}
