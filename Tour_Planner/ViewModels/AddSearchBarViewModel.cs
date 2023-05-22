using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tour_Planner.Model;

namespace Tour_Planner.ViewModels {
    public class AddSearchBarViewModel : INotifyPropertyChanged {

        public event PropertyChangedEventHandler? PropertyChanged;
        
        private string searchText = "Enter tour name...";

        public string SearchText {
            get => searchText;
            set {
                searchText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SearchText)));
            }
        }

        public ICommand SearchCommand { get; }

        public AddSearchBarViewModel() {
            // TODO: observer pattern
            SearchCommand = new RelayCommand(param => ExecuteSearch());
        }

        private void ExecuteSearch() {
            SearchRequested?.Invoke(this, SearchText);
        }

        public ObservableCollection<TourItem> FilterItems(ObservableCollection<TourItem> allTours)
        {
            ObservableCollection<TourItem> filteredTours = new ObservableCollection<TourItem>(
                allTours.Where(tour => tour.Name.Contains(searchText))
            );
            

            return filteredTours;
        }

        public event EventHandler<string>? SearchRequested;
    }
}
