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
using System.Text;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1;
using System.Net;
using Tour_Planner.Model.Enums;

namespace Tour_Planner.BL {
    public class TourManager {
        private IDataManager DataManager;
        private MapCreator MapCreator = new();
        private readonly ILoggerWrapper logger = LoggerFactory.GetLogger();
        public event EventHandler? Saved;

        public TourManager(IDataManager dataManager) {
            this.DataManager = dataManager;
        }

        public async Task AddTour(TourItem t) {
            logger.Info("Beginning to add a tour...");
            var tourcreation = await MapCreator.CreateMap(t.From, t.To, t.TransportType);
            t.Distance = tourcreation.Distance;
            t.EstimatedTime = tourcreation.EstimatedTime;
            t.TourInfo = tourcreation.Picture;
            DataManager.AddTour(t);
            logger.Info("Tour added");
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

        public async Task AddTourLog(TourLogs tl) {
            logger.Info($"Beginning to add tour log for tour with id {tl.TourId}");
            tl.DateTime = tl.DateTime.ToUniversalTime();
            DataManager.AddTourLog(tl);
            logger.Info($"Tour log added successfully");
        }

        public async Task EditTourLog(TourLogs tl) {
            logger.Info($"Beginning to edit tour log with id {tl.Id}");
            if(tl.DateTime.Kind != DateTimeKind.Utc) {
                tl.DateTime = tl.DateTime.ToUniversalTime();
            }
            DataManager.EditTourLog(tl);
            logger.Info("Tour log edited successfully");
        }

        public async Task DeleteTourLog(TourLogs tl) {
            logger.Info($"Beginning to delete tour log with id {tl.Id}");
            DataManager.DeleteTourLog(tl);
            logger.Info("Tour log deleted successfully");
        }

        public void ReportSpecificTour(TourItem t, ObservableCollection<TourLogs> tls) {
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
                .SetFontSize(20);
            document.Add(tourLogsHeader);

            int i = 1;
            foreach (var log in tls) {
                Paragraph tourLogHeader = new Paragraph($"{i}. Tourlog:")
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD))
                    .SetFontSize(18);
                document.Add(tourLogHeader);

                Paragraph tourLog = new Paragraph($"Date: {log.DateTime}\n" +
                                                  $"Comment: {log.Comment}\n" +
                                                  $"Difficulty: {log.Difficulty}\n" +
                                                  $"Total time: {log.TotalTime}\n" +
                                                  $"Rating: {log.Rating}")
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN))
                    .SetFontSize(12);
                document.Add(tourLog);
                
                i++;
            }

            document.Close();

            logger.Info("Finished generating PDF...");
            logger.Info("Opening PDF...");

            using Process fileopener = new Process();
            fileopener.StartInfo.FileName = "explorer";
            fileopener.StartInfo.Arguments = $"\"{TARGET_PDF}\"";
            fileopener.Start();

        }

        public void ReportAllTours(ObservableCollection<TourItem> ts, ObservableCollection<TourLogs> tls) {
            logger.Info("Creating tour report for all tours.");

            string TARGET_PDF = "collective_report.pdf";

            PdfWriter writer = new PdfWriter(TARGET_PDF);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);

            Paragraph header = new Paragraph("Tour Report for all tours")
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD))
                .SetFontSize(20);
            document.Add(header);

            int i = 1;
            foreach (TourItem t in ts) {
                Paragraph tourHeader = new Paragraph($"{i}. Tour: {t.Name}")
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

                Paragraph childFriendlinessHeader = new Paragraph("Child friendly?")
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD))
                    .SetFontSize(16);
                document.Add(childFriendlinessHeader);

                ObservableCollection<TourLogs> tsTl = new ObservableCollection<TourLogs>();
                foreach (var log in tls.Where(log => log.TourId == t.Id)) {
                    tsTl.Add(log);
                }
                
                Paragraph childFriendly = new Paragraph(isChildFriendly(tsTl) == true ? "Yes" : "No");
                document.Add(childFriendly);
                i++;
            }

            document.Close();
            
            logger.Info("Finished generating PDF...");
            logger.Info("Opening PDF...");

            using Process fileopener = new Process();
            fileopener.StartInfo.FileName = "explorer";
            fileopener.StartInfo.Arguments = $"\"{TARGET_PDF}\"";
            fileopener.Start();
        }

        public bool isChildFriendly(ObservableCollection<TourLogs> tls) {
            int items = tls.Count;
            int rating = 0;
            int difficulty = 0;
            int totaltime = 0;
            
            if (items == 0) {
                return false;
            }

            foreach (var log in tls) {
                rating += log.Rating;
                
                difficulty += (int)log.Difficulty;
                
                int days = log.TotalTime.Days;
                int hours = log.TotalTime.Hours;
                int minutes = log.TotalTime.Minutes;
                int seconds = log.TotalTime.Seconds;
                totaltime += (days * 24 * 60 * 60) + (hours * 60 * 60) + (minutes * 60) + seconds;
            }

            double avgRating = rating / items;
            double avgDifficulty = difficulty / items;
            double avgTotalTime = totaltime / items;

            // if on average rating is better or equal to 5, difficulty is lower than medium (2)
            // and the total time is less than 12 hours - the tour is child friendly
            return avgRating >= 5 && avgDifficulty < 2 && avgTotalTime < 43201;
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

        public void ImportToursFromCSV(string FilePath) {
            logger.Info("Importing tours from CSV...");
            string[] lines = File.ReadAllLines(FilePath);
            IEnumerable<TourItem> allItems = DataManager.GetTours();

            for (int i = 1; i < lines.Length; i++) {
                TourItem t = new TourItem();
                string[] content = lines[i].Split(',');

                t.Name = content[0];
                t.Description = content[1];
                t.From = content[2];
                t.To = content[3];
                t.TransportType = (Transport)Enum.Parse(typeof(Transport), content[4]);

                // if item t not found in allItems proceed with DataManager.AddTour(t)
                if (!allItems.Any(x => x.Name == t.Name)) {
                    logger.Info($"Importing tour {t.Name}");
                    //AddTour(t);
                    AddTour(t).ContinueWith(task => Saved?.Invoke(this, EventArgs.Empty));
                } else {
                    logger.Info($"Omitted importing existing tour item! ({t.Name})");
                }
            }
        }

        public void ExportToursToCSV() {
            logger.Info("Exporting tours to CSV...");
            logger.Info("Getting tours from DB");
            IEnumerable<TourItem> tours = DataManager.GetTours();

            if (tours.Count() == 0) {
                logger.Error("No tours found!");
                return;
            }

            string filepath = "exported_tours.csv";
            StringBuilder csvData = new StringBuilder();
            csvData.Append("Name,Description,From,To,TransportType,TourInfo,Distance,EstimatedTime\n");

            foreach (TourItem t in tours) {
                csvData.Append($"{t.Name},{t.Description},{t.From},{t.To},{t.TransportType},{t.TourInfo},{t.Distance},{t.EstimatedTime}\n");
            }

            File.WriteAllText(filepath, csvData.ToString());
        }
    }
}