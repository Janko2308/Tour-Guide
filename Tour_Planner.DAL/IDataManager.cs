using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tour_Planner.Model;

namespace Tour_Planner.DAL {
    public interface IDataManager {
        public void AddTour(TourItem t);
        public void EditTour(TourItem t);
        public void DeleteTour(TourItem t);
        public void AddTourLog(TourLogs tl);
        public void EditTourLog(TourLogs tl);
        public void DeleteTourLog(TourLogs tl);
        public IEnumerable<TourItem> GetTours();
        public IEnumerable<TourLogs> GetTourLogs();
    }
}
