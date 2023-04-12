using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tour_Planner.Model;

namespace Tour_Planner.DAL {
    public interface IDataManager {
        public void AddTour(TourItem t);
        public IEnumerable<TourItem> GetTours();
    }
}
