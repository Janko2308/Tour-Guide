using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tour_Planner.BL;
using Tour_Planner.Model;

namespace Tour_Planner.ViewModels {
    public class AddNewTourLogViewModel : BaseViewModel {
        private TourManager bl;
        public TourLogs TourLog { get; private set; } = new();
        public ICommand ExecuteCommandAdd { get; }
        public ICommand ExecuteCommandEdit { get;}

        public AddNewTourLogViewModel(TourManager bl) {
            this.bl = bl;

            ExecuteCommandAdd = new RelayCommand(param => {
                // TODO: Adding new tour log - suggestion: base on adding tours
            });

            ExecuteCommandEdit = new RelayCommand(param => {
                // TODO: Editing tour log - suggestion: base on editing tours
            });
        }
    }
}
