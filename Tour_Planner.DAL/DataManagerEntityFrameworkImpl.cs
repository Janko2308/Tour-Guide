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

        public void EditTour(TourItem t) {
            // find item in DB with the same Id as t.Id
            TourItem tour = context.TourItems.Find(t.Id);
            if (tour == null) {
                throw new Exception("Tour not found");
            }
            
            // change the values of the entity in DB with the t.Id Id value, to the values contained in t
            tour.Name = t.Name;
            tour.Description = t.Description;
            tour.From = t.From;
            tour.To = t.To;
            tour.TourInfo = t.TourInfo;
            tour.Distance = t.Distance;
            tour.EstimatedTime = t.EstimatedTime;

            context.SaveChanges();
        }

        public bool DeleteTour(TourItem t) {
            // TODO: DELETE
            return false;
        }

        public IEnumerable<TourItem> GetTours() {
            context.TourItems.Load();
            return context.TourItems;
        }
    }
}
