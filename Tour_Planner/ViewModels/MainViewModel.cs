using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Crypto;
using Tour_Planner.BL;
using Tour_Planner.Model;
using Microsoft.Win32;
using log4net;
using System.Runtime.ExceptionServices;


namespace Tour_Planner.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged {
        private AddSearchBarViewModel addSearchBarVM;
        private AddNewTourViewModel addNewTourVM;

        public event PropertyChangedEventHandler? PropertyChanged;

        public EventHandler? DbChanged;
        public EventHandler? tourChanged;


        private TourItem selectedTour;
        private ObservableCollection<TourItem> tours;
        private ObservableCollection<TourItem> filteredTours;
        private ObservableCollection<TourItem> allTours;

        public ObservableCollection<TourItem> Tours { 
            get => tours; 
            set {
                tours = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Tours)));
            } 
        }

        public ObservableCollection<TourLogs> AllTourLogs { get; set; }

        private ObservableCollection<TourLogs> tourLogs;
        public ObservableCollection<TourLogs> TourLogs {
            get => tourLogs;
            set {
                tourLogs = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TourLogs)));
            }
        }

        public string filename;
        
        public string Filename { get; private set; }

        public TourItem SelectedTour {
            get => selectedTour;
            set {
                selectedTour = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedTour)));
                UpdateTourLogs();
            }
        }

        private TourLogs selectedTourLog;

        public TourLogs SelectedTourLog {
            get => selectedTourLog;
            set {
                selectedTourLog = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedTourLog)));
            }
        }

        
        public ObservableCollection<TourLogs> TourLogsOfSelectedTour {
            get => tourLogs;
            set {
                tourLogs = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TourLogsOfSelectedTour)));
            }
        }
        
        public ObservableCollection<TourItem> FilteredTours { 
            get => filteredTours;
            set { 
                filteredTours = value;
            }
        }

        private void OnSearchRequested(object sender, string searchText)
        {
            this.Tours = this.allTours;
            ObservableCollection<TourItem> filteredTours = new ObservableCollection<TourItem>(this.Tours.Where(tour => tour.Name.Contains(searchText)));
            if(filteredTours.Count != 0) {
                this.Tours = filteredTours;
            }
            OnPropertyChanged(nameof(Tours));
        }

        public MainViewModel(AddSearchBarViewModel asbVM, AddNewTourViewModel antVM, TourManager bl) {
            this.addSearchBarVM = asbVM;
            this.addNewTourVM = antVM;
            this.Tours = new ObservableCollection<TourItem>(bl.GetTours());
            this.allTours = new ObservableCollection<TourItem>(bl.GetTours());
            this.AllTourLogs = new ObservableCollection<TourLogs>(bl.GetTourLogs());
            this.addSearchBarVM.SearchRequested += OnSearchRequested;
            this.selectedTour = this.Tours.FirstOrDefault();
            this.TourLogsOfSelectedTour = new ObservableCollection<TourLogs>();
            //this.TourLogs is AllTourLogs where TourId == selectedTour.Id
            foreach (var log in AllTourLogs.Where(log => log.TourId == selectedTour.Id)) {
                this.TourLogsOfSelectedTour.Add(log);
            }

            this.selectedTourLog = this.TourLogsOfSelectedTour.FirstOrDefault();


            ExecuteCommandOpenNewTour = new RelayCommand(param => {
                var dialog = new Views.AddNewTour();
                ((AddNewTourViewModel)dialog.DataContext).Saved += (sender, args) => this.Tours = new ObservableCollection<TourItem>(bl.GetTours());
                dialog.ShowDialog();
            });
            
            ExecuteCommandOpenNewTourLog = new RelayCommand(param => {
                try {
                    if (selectedTour == null) {
                        MessageBox.Show("Please select a tour first");
                    } else {
                        var dialog = new Views.AddNewTourLog(selectedTour.Id);
                        ((AddNewTourLogViewModel)dialog.DataContext).Saved += (sender, args) => this.TourLogs = new ObservableCollection<TourLogs>(bl.GetTourLogs());
                        dialog.ShowDialog();
                    }
                }
                catch(Exception e) {
                    MessageBox.Show(e.Message);
                }
            });
            
            ExecuteCommandOpenEditTour = new RelayCommand(param => {
                try {
                    if (selectedTour == null) {
                        MessageBox.Show("Please select a tour to edit");
                    } else {
                        TourItem t = selectedTour;
                        var dialog = new Views.AddNewTour(t);
                        ((AddNewTourViewModel)dialog.DataContext).Saved += (sender, args) => this.Tours = new ObservableCollection<TourItem>(bl.GetTours());
                        dialog.ShowDialog();
                    }
                }
                catch(Exception e) {
                    MessageBox.Show(e.Message);
                }
            });

            ExecuteCommandOpenEditTourLog = new RelayCommand(param => {
                try {
                    if (selectedTourLog == null) {
                        MessageBox.Show("No tour log selected!");
                    } else {
                        TourLogs t = selectedTourLog;
                        var dialog = new Views.AddNewTourLog(t);
                        ((AddNewTourLogViewModel)dialog.DataContext).Saved += (sender, args) => this.TourLogs = new ObservableCollection<TourLogs>(bl.GetTourLogs());
                        dialog.ShowDialog();
                    }
                }
                catch (Exception e) {
                    MessageBox.Show(e.Message);
                }
            });
            
            ExecuteCommandDeleteThisTour = new RelayCommand(param => {
                try {
                    if (selectedTour == null) {
                        MessageBox.Show("Please select a tour to delete");
                        return;
                    }

                    var yesOrNo = MessageBox.Show($"Do you really want to delete Tour '{SelectedTour.Name}'?", "Confirmation", MessageBoxButton.YesNo);

                    if (yesOrNo == MessageBoxResult.Yes) {
                        bl.DeleteTour(SelectedTour);
                        SelectedTour = Tours.FirstOrDefault();
                    }
                    Tours = new ObservableCollection<TourItem>(bl.GetTours());
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                }
            });

            ExecuteCommandDeleteThisTourLog = new RelayCommand(param => {
                try {
                    if (selectedTourLog == null) {
                        MessageBox.Show("No tour log selected!");
                        return;
                    }

                    var yesOrNo = MessageBox.Show($"Do you really want to delete this tour log? Comment name: {selectedTourLog.Comment}", "Confirmation", MessageBoxButton.YesNo);

                    if (yesOrNo == MessageBoxResult.Yes) {
                        bl.DeleteTourLog(SelectedTourLog);
                        this.TourLogs = new ObservableCollection<TourLogs>(bl.GetTourLogs());
                        SelectedTourLog = TourLogs.FirstOrDefault();

                    }
                }
                catch (Exception e) {
                    MessageBox.Show(e.Message);
                }
            });

            ExecuteCommandGenerateReportSpecificTour = new RelayCommand(param => {
                try {
                    bl.ReportSpecificTour(SelectedTour, TourLogsOfSelectedTour);
                }
                catch (Exception e) {
                    MessageBox.Show(e.Message);
                }
            });

            ExecuteCommandGenerateReportAllTours = new RelayCommand(param => {
                try {
                    bl.ReportAllTours(Tours, AllTourLogs);
                }
                catch (Exception e) {
                    MessageBox.Show(e.Message);
                }
            });

            ExecuteCommandExportCSV = new RelayCommand(param => {
                try {
                    bl.ExportToursToCSV();
                    MessageBox.Show("Exported successfully!");
                }
                catch (Exception e) {
                    MessageBox.Show(e.Message);
                }
            });

            ExecuteCommandImportCSV = new RelayCommand(param => {
                try {
                    MessageBox.Show("Remember, this csv cannot have any data considering distance, " +
                        "map or estimated time - the app adds the tour as new, " +
                        "and as such creates this data itself");
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = "CSV files (*.csv)|*.csv";

                    if (openFileDialog.ShowDialog() == true) {
                        string selectedFile = openFileDialog.FileName;
                        bl.ImportToursFromCSV(selectedFile);
                        bl.Saved += (sender, args) => this.Tours = new ObservableCollection<TourItem>(bl.GetTours());
                        MessageBox.Show("Tours added successfully!");
                    }
                }
                catch (Exception e) {
                    MessageBox.Show(e.Message);
                }
            });
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public object ConvertTimeToFormattedString(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (value is TimeSpan timeSpan) {
                string days = timeSpan.Days.ToString("00");
                string hours = timeSpan.Hours.ToString("00");
                string minutes = timeSpan.Minutes.ToString("00");
                string seconds = timeSpan.Seconds.ToString("00");

                return $"{days} days, {hours} hours, {minutes} minutes, {seconds} seconds";
            }

            return string.Empty;
        }

        private void UpdateTourLogs() {
            if (selectedTour != null) {
                TourLogsOfSelectedTour.Clear();
                foreach (var log in AllTourLogs.Where(log => log.TourId == selectedTour.Id)) {
                    TourLogsOfSelectedTour.Add(log);
                }

                selectedTourLog = TourLogsOfSelectedTour.FirstOrDefault();
            }
        }


        public ICommand ExecuteCommandOpenNewTour { get; }
        public ICommand ExecuteCommandOpenNewTourLog { get; }
        public ICommand ExecuteCommandOpenEditTour { get; }
        public ICommand ExecuteCommandOpenEditTourLog { get; }
        public ICommand ExecuteCommandDeleteThisTour { get; }
        public ICommand ExecuteCommandDeleteThisTourLog { get; }
        public ICommand ExecuteCommandGenerateReportSpecificTour { get; }
        public ICommand ExecuteCommandGenerateReportAllTours { get; }
        public ICommand ExecuteCommandExportCSV { get; }
        public ICommand ExecuteCommandImportCSV { get; }

    }
}
