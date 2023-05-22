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
            // check if the t.name already exists
            if (context.TourItems.Any(x => x.Name == t.Name)) {
                throw new DbUpdateException($"Tour name {t.Name} already used");
            }
            
            context.TourItems.Add(t);
            context.SaveChanges();
        }

        public void EditTour(TourItem t) {
            // find item in DB with the same Id as t.Id
            TourItem tour = context.TourItems.Find(t.Id);

            if (tour == null) {
                throw new KeyNotFoundException("Tour not found");
            }

            // check if the t.name does not collide with any other tour besides the one we are editing
            if (context.TourItems.Any(x => x.Name == t.Name && x.Id != t.Id)) {
                throw new DbUpdateException($"Tour name {t.Name} already used");
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

        public void DeleteTour(TourItem t) {
            // TODO: DELETE
            context.TourItems.Remove(t);
            context.SaveChanges();
        }

        public void AddTourLog(TourLogs tl) {
            // check if such tourlog already exists (i.e. same tour, same date and time)
            if (context.TourLogItems.Any(x => x.TourId == tl.TourId && x.DateTime == tl.DateTime)) {
                throw new DbUpdateException($"TourLog for tour {tl.TourId} on date {tl.DateTime} already exists");
            }
            context.TourLogItems.Add(tl);
            context.SaveChanges();
        }

        public void EditTourLog(TourLogs tl) {
            TourLogs tourLog = context.TourLogItems.Find(tl.Id);
            
            if (tourLog == null) {
                throw new KeyNotFoundException("TourLog not found");
            }

            // check if such tourlog already exists (i.e. same tour, same date)
            if (context.TourLogItems.Any(x => x.TourId == tl.TourId && x.DateTime == tl.DateTime && x.Id != tl.Id)) {
                throw new DbUpdateException($"TourLog for tour {tl.TourId} on date {tl.DateTime} already exists");
            }

            // change the values
            tourLog.Comment = tl.Comment;
            tourLog.Difficulty = tl.Difficulty;
            tourLog.TotalTime = tl.TotalTime;
            tourLog.Rating = tl.Rating;

            context.SaveChanges();
        }

        public void DeleteTourLog(TourLogs tl) {
            context.TourLogItems.Remove(tl);
            context.SaveChanges();
        }

        public IEnumerable<TourItem> GetTours() {
            context.TourItems.Load();
            return context.TourItems;
        }

        public IEnumerable<TourLogs> GetTourLogs() {
            context.TourLogItems.Load();
            return context.TourLogItems;
        }
    }
}
