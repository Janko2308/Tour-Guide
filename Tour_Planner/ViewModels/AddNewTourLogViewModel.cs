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
        // public event EventHandler<TourLogs> AddTourLogButtonClicked;

        public AddNewTourLogViewModel(TourManager bl, int TourId) {
            this.TourLog.TourId = TourId;
            this.bl = bl;
            this.IsAdded = true;

            ExecuteCommandAdd = new RelayCommand(param => {
                try {
                    bl.AddTourLog(TourLog);
                }
                catch(Exception e) {
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
                    bl.EditTourLog(TourLog);
                }
                catch(Exception e) {
                    MessageBox.Show(e.Message);
                }
            });
        }
    }
}
