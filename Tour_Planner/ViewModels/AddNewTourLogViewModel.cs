using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tour_Planner.BL;
using Tour_Planner.Model;

namespace Tour_Planner.ViewModels {
    public class AddNewTourLogViewModel : BaseViewModel {
        private TourManager bl;
        public TourLogs TourLog { get; private set; } = new();
        public ICommand ExecuteCommandAdd { get; }
        public ICommand ExecuteCommandEdit { get;}
        public bool IsEdited { get; private set; } = false;
        public bool IsAdded { get; private set; } = false;
        public event EventHandler? Saved;

        public AddNewTourLogViewModel(TourManager bl, int TourId) {
            this.TourLog.TourId = TourId;
            this.bl = bl;
            this.IsAdded = true;

            ExecuteCommandAdd = new RelayCommand(param => {
                try {
                    if (TourLog.TotalTime == TimeSpan.Zero || String.IsNullOrEmpty(TourLog.Comment)) {
                        throw new ArgumentNullException("Please fill all fields!");
                    }

                    if (!TimeSpan.TryParse(TourLog.TotalTime.ToString(), out TimeSpan time)) {
                        throw new ArgumentException("Please enter a valid time!");
                    }

                    if (TourLog.Rating < 1 || TourLog.Rating > 10) {
                        throw new ArgumentOutOfRangeException("Rating must be between 1 and 10!");
                    }

                    bl.AddTourLog(TourLog);
                    MessageBox.Show("TourLog added successfully");

                    foreach (Window window in Application.Current.Windows) {
                        if (window.DataContext == this) {
                            window.Close();
                        }
                    }
                }
                
                catch (Exception e) {
                    MessageBox.Show(e.Message);
                }
            });
        }

        public AddNewTourLogViewModel(TourManager bl, TourLogs selectedTL) {
            this.bl = bl;
            this.TourLog = selectedTL;
            this.IsEdited = true;

            ExecuteCommandEdit = new RelayCommand(param => {
                try {
                    if (TourLog.TotalTime == TimeSpan.Zero || TourLog.Rating < 0 || String.IsNullOrEmpty(TourLog.Comment)) {
                        throw new ArgumentNullException("Please fill all fields!");
                    }

                    if (!TimeSpan.TryParse(TourLog.TotalTime.ToString(), out TimeSpan time)) {
                        throw new ArgumentException("Please enter a valid time!");
                    }

                    if (TourLog.Rating < 1 || TourLog.Rating > 10) {
                        throw new ArgumentOutOfRangeException("Rating must be between 1 and 10!");
                    }

                    bl.EditTourLog(TourLog);
                    MessageBox.Show("Tour log edited successfully");
                    foreach (Window window in Application.Current.Windows) {
                        if (window.DataContext == this) {
                            window.Close();
                        }
                    }
                }
                catch(Exception e) {
                    MessageBox.Show(e.Message);
                }
            });
        }
    }
}
