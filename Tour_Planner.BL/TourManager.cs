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
            MapCreator.CreateMap(t.From, t.To, t.TransportType).ContinueWith(res => {
                t.Distance = res.Result.Distance;
                t.EstimatedTime = res.Result.EstimatedTime;
                t.TourInfo = res.Result.Picture;
                DataManager.AddTour(t);
            });
        }

        public void EditTour(TourItem t) {
            MapCreator.CreateMap(t.From, t.To, t.TransportType).ContinueWith(res => {
                t.Distance = res.Result.Distance;
                t.EstimatedTime = res.Result.EstimatedTime;
                t.TourInfo = res.Result.Picture;
                DataManager.EditTour(t);
            });
        }

        public bool DeleteTour(TourItem t) {
            return DataManager.DeleteTour(t);
        }

        public IEnumerable<TourItem> GetTours() {
            return DataManager.GetTours();
        }
    }
}