using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Crypto;
using Tour_Planner.DAL;
using Tour_Planner.Model;
using Tour_Planner.Model.Structs;
using iText.IO.Image;

namespace Tour_Planner.BL {
    public class TourManager {
        private IDataManager DataManager;
        private MapCreator MapCreator = new();
        private readonly ILoggerWrapper logger = LoggerFactory.GetLogger();

        public TourManager(IDataManager dataManager) {
            this.DataManager = dataManager;
        }

        public void AddTour(TourItem t) {
            logger.Info("Beginning to add a tour...");
            MapCreator.CreateMap(t.From, t.To, t.TransportType).ContinueWith(res => {
                t.Distance = res.Result.Distance;
                t.EstimatedTime = res.Result.EstimatedTime;
                t.TourInfo = res.Result.Picture;
                DataManager.AddTour(t);
                logger.Info("Tour added");
            });
        }


        public void EditTour(TourItem t) {
            logger.Info($"Beginning to edit tour with id {t.Id}");
            MapCreator.CreateMap(t.From, t.To, t.TransportType).ContinueWith(res => {
                t.Distance = res.Result.Distance;
                t.EstimatedTime = res.Result.EstimatedTime;
                t.TourInfo = res.Result.Picture;
                DataManager.EditTour(t);
                logger.Info("Tour edited successfully");
            });
        }

        public void DeleteTour(TourItem t) {
            logger.Info($"Beginning to delete tour with id {t.Id}");
            DataManager.DeleteTour(t);
            logger.Info("Tour deleted successfully");
        }

        public void AddTourLog(TourLogs tl) {
            logger.Info($"Beginning to add tour log for tour with id {tl.TourId}");
            //tl.DateTime = ConvertUTC(tl.DateTime);
            tl.DateTime.ToUniversalTime();
            DataManager.AddTourLog(tl);
            logger.Info($"Tour log added successfully");
        }

        public void EditTourLog(TourLogs tl) {
            logger.Info($"Beginning to edit tour log with id {tl.Id}");
            tl.DateTime = ConvertUTC(tl.DateTime);
            DataManager.EditTourLog(tl);
            logger.Info("Tour log edited successfully");
        }

        public void DeleteTourLog(TourLogs tl) {
            logger.Info($"Beginning to delete tour log with id {tl.Id}");
            DataManager.DeleteTourLog(tl);
            logger.Info("Tour log deleted successfully");
        }

        public void ReportSpecificTour(TourItem t) {
            logger.Info($"Creating tour report for tour {t.Name}.");

            string filename = t.Name.ToLower().Replace(" ", "_");
            
            string TARGET_PDF = $"report_{filename}.pdf";
            
            PdfWriter writer = new PdfWriter(TARGET_PDF);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);

            Paragraph header = new Paragraph($"Tour Report for {t.Name}")
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD))
                .SetFontSize(20);
            document.Add(header);

            Paragraph tourInfo = new Paragraph($"From {t.From} to {t.To} by {t.TransportType}.\n" +
                                               $"Distance: {t.Distance} km\n" +
                                               $"Estimated time: {t.EstimatedTime} min\n" +
                                               $"Description: {t.Description}")
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN))
                .SetFontSize(12);
            document.Add(tourInfo);
            
            Image tourImage = new Image(ImageDataFactory.Create(t.TourInfo));
            document.Add(tourImage);

            Paragraph tourLogsHeader = new Paragraph("Tour Logs")
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD))
                .SetFontSize(16);
            document.Add(tourLogsHeader);

            Paragraph temp = new Paragraph("Temporarily no tour logs.");
            document.Add(temp);

            document.Close();

            logger.Info("Finished generating PDF...");
            logger.Info("Opening PDF...");

            using Process fileopener = new Process();
            fileopener.StartInfo.FileName = "explorer";
            fileopener.StartInfo.Arguments = $"\"{TARGET_PDF}\"";
            fileopener.Start();

        }

        public void ReportAllTours(ObservableCollection<TourItem> ts) {
            logger.Info("Creating tour report for all tours.");

            string TARGET_PDF = "collective_report.pdf";

            PdfWriter writer = new PdfWriter(TARGET_PDF);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);

            Paragraph header = new Paragraph("Tour Report for all tours")
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD))
                .SetFontSize(20);
            document.Add(header);

            foreach (TourItem t in ts) {
                Paragraph tourHeader = new Paragraph($"Tour: {t.Name}")
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD))
                    .SetFontSize(18);
                document.Add(tourHeader);

                Paragraph tourInfo = new Paragraph($"From {t.From} to {t.To} by {t.TransportType}.\n" +
                                                   $"Distance: {t.Distance} km\n" +
                                                   $"Estimated time: {t.EstimatedTime} min\n" +
                                                   $"Description: {t.Description}")
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN))
                    .SetFontSize(12);
                document.Add(tourInfo);

                Paragraph tourLogsHeader = new Paragraph("Tour Logs")
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD))
                    .SetFontSize(16);
                document.Add(tourLogsHeader);

                Paragraph temp = new Paragraph("Temporarily no tour logs.");
                document.Add(temp);
            }

            document.Close();
            
            logger.Info("Finished generating PDF...");
            logger.Info("Opening PDF...");

            using Process fileopener = new Process();
            fileopener.StartInfo.FileName = "explorer";
            fileopener.StartInfo.Arguments = $"\"{TARGET_PDF}\"";
            fileopener.Start();
        }

        public IEnumerable<TourItem> GetTours() {
            logger.Info("Getting tours...");
            return DataManager.GetTours();
        }

        public IEnumerable<TourLogs> GetTourLogs() {
            logger.Info("Getting tour logs...");
            return DataManager.GetTourLogs();
        }

        public DateTime ConvertUTC(DateTime dt) {
            logger.Info("Converting UTC...");
            return TimeZoneInfo.ConvertTimeFromUtc(dt, TimeZoneInfo.Local);
        }

        // TODO: test function, no calls yet
        public DateTime ConvertLocal(DateTime dt) {
            logger.Info("Converting local...");
            return TimeZoneInfo.ConvertTimeToUtc(dt, TimeZoneInfo.Local);
        }
    }
}