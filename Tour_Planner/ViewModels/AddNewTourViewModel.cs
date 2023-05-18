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

        public AddNewTourViewModel(TourManager bl) {
            this.bl = bl;
            this.IsEdited = false;
            
            ExecuteCommandAdd = new RelayCommand(param => {
                try {
                    bl.AddTour(Tour);
                    /*Window current = Window.GetWindow((AddNewTour)param);
                    current.Close();*/
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
                    bl.EditTour(Tour);
                    /*Window current = Window.GetWindow((AddNewTour)param);
                    current.Close();*/
                }
                catch(Exception e) {
                    MessageBox.Show(e.Message);
                }
                // TODO: Command for edit, and overthink how AddNewTour works
                // suggestion: it is the same form, but with different buttons
                // one with "Add" should call addtour from bl and add a new tour to the db
                // other one with "Edit" should call edittour and edit the tour which it gets at the beginning
            });
        }
        
        public ICommand ExecuteCommandAdd { get; }
        public ICommand ExecuteCommandEdit { get; }
        
        public event EventHandler<TourItem> AddTourButtonClicked;
    }
}
