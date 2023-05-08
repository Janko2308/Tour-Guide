using Tour_Planner.DAL;
using Tour_Planner.Model;

namespace Tour_Planner.BL {
    public class TourManager {
        private IDataManager DataManager;
        private MapCreator MapCreator;

        public TourManager(IDataManager dataManager) {
            this.DataManager = dataManager;
        }

        public void AddTour(TourItem t) {
            t.TourInfo = MapCreator.CreateMap(t.From, t.To).Result;
            DataManager.AddTour(t);
        }

        public IEnumerable<TourItem> GetTours() {
            return DataManager.GetTours();
        }
    }
}