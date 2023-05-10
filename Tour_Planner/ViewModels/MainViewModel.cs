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

        private TourItem selectedTour;

        public TourItem SelectedTour {
            get => selectedTour;
            set {
                selectedTour = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedTour)));
                Console.WriteLine("Changing");
            }
        }

        public MainViewModel(AddSearchBarViewModel asbVM, AddNewTourViewModel antVM, TourManager bl) {
            this.addSearchBarVM = asbVM;
            this.addNewTourVM = antVM;
            this.Tours = new ObservableCollection<TourItem>(bl.GetTours());
            // this.selectedTour is equal if this.Tours contain no entries then empty, else this.Tours[0]
            this.selectedTour = this.Tours.FirstOrDefault();

            ExecuteCommandOpenNewTour = new RelayCommand(param => new Views.AddNewTour().ShowDialog());
        }
       

        public ICommand ExecuteCommandOpenNewTour { get; }

        public ObservableCollection<TourItem> Tours { get; set; }

    }
}
