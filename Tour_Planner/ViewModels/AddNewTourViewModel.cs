using Npgsql.Internal.TypeHandlers.LTreeHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tour_Planner.BL;
using Tour_Planner.Model;
using Tour_Planner.Views;

namespace Tour_Planner.ViewModels {
    public class AddNewTourViewModel : BaseViewModel {
        private TourManager bl;
        public TourItem Tour { get; private set; } = new();
        public bool IsEdited { get; private set; } = false;
        public bool IsAdded { get; private set; } = false;

        public AddNewTourViewModel(TourManager bl) {
            this.bl = bl;
            this.IsAdded = true;
            
            ExecuteCommandAdd = new RelayCommand(param => {
                try {
                    if (String.IsNullOrEmpty(Tour.Name) || String.IsNullOrEmpty(Tour.Description) || String.IsNullOrEmpty(Tour.From) || 
                        String.IsNullOrEmpty(Tour.To)) {
                            throw new Exception("All fields must be filled in!");
                    }
                    
                    bl.AddTour(Tour);
                    MessageBox.Show("Tour added successfully!");
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

        public AddNewTourViewModel(TourManager bl, TourItem selectedTour) {
            this.bl = bl;
            this.Tour = selectedTour;
            this.IsEdited = true;

            ExecuteCommandEdit = new RelayCommand(param => {
                try {
                    if (String.IsNullOrEmpty(Tour.Name) || String.IsNullOrEmpty(Tour.Description) || String.IsNullOrEmpty(Tour.From) ||
                        String.IsNullOrEmpty(Tour.To)) {
                        throw new Exception("All fields must be filled in!");
                    }
                    
                    bl.EditTour(Tour);
                    MessageBox.Show("Tour data updated successfully!");
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
        
        public ICommand ExecuteCommandAdd { get; }
        public ICommand ExecuteCommandEdit { get; }
        
        //public event EventHandler<TourItem> AddTourButtonClicked;
    }
}
