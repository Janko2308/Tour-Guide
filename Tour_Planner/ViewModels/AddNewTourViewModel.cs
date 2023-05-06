using Npgsql.Internal.TypeHandlers.LTreeHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tour_Planner.BL;
using Tour_Planner.Model;

namespace Tour_Planner.ViewModels {
    public class AddNewTourViewModel : BaseViewModel {
        private TourManager bl;
        public TourItem Tour { get; private set; } = new();

        public AddNewTourViewModel(TourManager bl) {
            this.bl = bl;
            //ExecuteCommandAdd = new RelayCommand(param => AddTourButtonClicked?.Method.Invoke(this, null));
            // ExecuteCommandAdd uses RelayCommand to send a request to bl function called AddTour(TourItem tour)
            ExecuteCommandAdd = new RelayCommand(param => bl.AddTour(Tour));
        }
        
        public ICommand ExecuteCommandAdd { get; }
        
        public event EventHandler<TourItem> AddTourButtonClicked;
    }
}
