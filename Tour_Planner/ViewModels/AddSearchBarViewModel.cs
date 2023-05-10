using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Tour_Planner.ViewModels {
    public class AddSearchBarViewModel : INotifyPropertyChanged {

        public event PropertyChangedEventHandler? PropertyChanged;
        
        private string searchText = "temporary, to delete";

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
            SearchCommand = new RelayCommand(param => Debug.Print(searchText));
        }
    }
}
