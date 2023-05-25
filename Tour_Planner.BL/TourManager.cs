using Tour_Planner.DAL;
using Tour_Planner.Model;
using Tour_Planner.Model.Structs;

namespace Tour_Planner.BL {
    public class TourManager {
        private IDataManager DataManager;
        private MapCreator MapCreator = new();
        private readonly ILoggerWrapper logger = LoggerFactory.GetLogger();

        public TourManager(IDataManager dataManager) {
            this.DataManager = dataManager;
        }

        public void AddTour(TourItem t) {
            try {
                logger.Info("Beginning to add a tour...");
                MapCreator.CreateMap(t.From, t.To, t.TransportType).ContinueWith(res => {
                    t.Distance = res.Result.Distance;
                    t.EstimatedTime = res.Result.EstimatedTime;
                    t.TourInfo = res.Result.Picture;
                    DataManager.AddTour(t);
                    logger.Info("Tour added");
                });
            }
            catch (Exception e) {
                logger.Fatal("Failed to add tour", e);
            }
        }


        public void EditTour(TourItem t) {
            try {
                logger.Info($"Beginning to edit tour with id {t.Id}");
                MapCreator.CreateMap(t.From, t.To, t.TransportType).ContinueWith(res => {
                    t.Distance = res.Result.Distance;
                    t.EstimatedTime = res.Result.EstimatedTime;
                    t.TourInfo = res.Result.Picture;
                    DataManager.EditTour(t);
                    logger.Info("Tour edited successfully");
                });
            }
            catch (Exception e) {
                logger.Fatal("Failed to edit tour", e);
            }
        }

        public void DeleteTour(TourItem t) {
            try {
                logger.Info($"Beginning to delete tour with id {t.Id}");
                DataManager.DeleteTour(t);
                logger.Info("Tour deleted successfully");
            }
            catch (Exception e) {
                logger.Fatal("Failed to delete tour", e);
            }
        }

        public void AddTourLog(TourLogs tl) {
            try {
                logger.Info($"Beginning to add tour log for tour with id {tl.TourId}");
                //tl.DateTime = ConvertUTC(tl.DateTime);
                tl.DateTime.ToUniversalTime();
                DataManager.AddTourLog(tl);
                logger.Info($"Tour log added successfully");
            }
            catch (Exception e) {
                logger.Fatal($"Failed to add tour log", e);
            }
        }

        public void EditTourLog(TourLogs tl) {
            try {
                logger.Info($"Beginning to edit tour log with id {tl.Id}");
                tl.DateTime = ConvertUTC(tl.DateTime);
                DataManager.EditTourLog(tl);
                logger.Info("Tour log edited successfully");
            }
            catch (Exception e) {
                logger.Fatal("Failed to edit tour log", e);
            }
        }

        public void DeleteTourLog(TourLogs tl) {
            try {
                logger.Info($"Beginning to delete tour log with id {tl.Id}");
                DataManager.DeleteTourLog(tl);
                logger.Info("Tour log deleted successfully");
            }
            catch (Exception e) {
                logger.Fatal("Failed to delete tour log", e);
            }
        }

        public IEnumerable<TourItem> GetTours() {
            try {
                logger.Info("Getting tours...");
                return DataManager.GetTours();
            }
            catch (Exception e) {
                logger.Fatal("Failed to get tours", e);
                return null;
            }
        }

        public IEnumerable<TourLogs> GetTourLogs() {
            try {
                logger.Info("Getting tour logs...");
                return DataManager.GetTourLogs();
            }
            catch (Exception e) {
                logger.Fatal("Failed to get tour logs", e);
                return null;
            }
        }

        public DateTime ConvertUTC(DateTime dt) {
            try {
                logger.Info("Converting UTC...");
                return TimeZoneInfo.ConvertTimeFromUtc(dt, TimeZoneInfo.Local);
            }
            catch (Exception e) {
                logger.Fatal("Failed to convert UTC", e);
                return dt;
            }
        }

        // TODO: test function, no calls yet
        public DateTime ConvertLocal(DateTime dt) {
            try {
                logger.Info("Converting local...");
                return TimeZoneInfo.ConvertTimeToUtc(dt, TimeZoneInfo.Local);
            }
            catch (Exception e) {
                logger.Fatal("Failed to convert local", e);
                return dt;
            }
        }
    }
}