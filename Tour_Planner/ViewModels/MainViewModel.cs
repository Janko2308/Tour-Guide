using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Tour_Planner.BL;
using Tour_Planner.Model;

namespace Tour_Planner.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged {
        private AddSearchBarViewModel addSearchBarVM;
        private AddNewTourViewModel addNewTourVM;

        public event PropertyChangedEventHandler? PropertyChanged;

        private string searchText = "temporary, to delete";

        public string SearchText {
            get => searchText;
            set {
                searchText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SearchText)));
            }
        }

        public MainViewModel(AddSearchBarViewModel asbVM, AddNewTourViewModel antVM, TourManager bl) {
            this.addSearchBarVM = asbVM;
            this.addNewTourVM = antVM;
            this.Tours = new ObservableCollection<TourItem>(bl.GetTours());
            SearchCommand = new RelayCommand(param => Debug.Print(searchText));

            ExecuteCommandOpenNewTour = new RelayCommand(param => new Views.AddNewTour().ShowDialog());
        }


        public ICommand SearchCommand {
            get;
        }

        public ICommand ExecuteCommandOpenNewTour { get; }

        public ObservableCollection<TourItem> Tours { get; set; }

    }
}
