using Tour_Planner.DAL;
using Tour_Planner.Model;
using Tour_Planner.Model.Structs;

namespace Tour_Planner.BL {
    public class TourManager {
        private IDataManager DataManager;
        private MapCreator MapCreator = new();

        public TourManager(IDataManager dataManager) {
            this.DataManager = dataManager;
        }

        public void AddTour(TourItem t) {
            TourCreation res = MapCreator.CreateMap(t.From, t.To, t.TransportType).Result;
            t.TourInfo = res.Picture;
            t.EstimatedTime = res.EstimatedTime;
            t.Distance = res.Distance;
            DataManager.AddTour(t);
        }

        public IEnumerable<TourItem> GetTours() {
            return DataManager.GetTours();
        }
    }
}