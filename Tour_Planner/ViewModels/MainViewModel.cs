using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tour_Planner.BL;
using Tour_Planner.Model;

namespace Tour_Planner.ViewModels
{
    class MainViewModel : INotifyPropertyChanged {
        private AddSearchBarViewModel addSearchBarVM;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string SearchText {
            get => searchText;
            set {
                searchText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SearchText)));
            }
        }

        public MainViewModel(AddSearchBarViewModel asbVM, TourManager bl) {
            this.addSearchBarVM = asbVM;
            this.Tours = new ObservableCollection<TourItem>(bl.GetTours());
            SearchCommand = new RelayCommand(param => Debug.Print(searchText));
        }


        public ICommand SearchCommand {
            get;

        }

        public ObservableCollection<TourItem> Tours { get; set; }
    }
}
