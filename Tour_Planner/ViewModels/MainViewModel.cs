﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Tour_Planner.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        private AddSearchBarViewModel addSearchBarVM;
        private AddTourManagementViewModel addTourManagementVM;

        public event PropertyChangedEventHandler? PropertyChanged;

        private string searchText = "banana";

        public string SearchText
        {
            get => searchText;
            set
            {
                searchText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SearchText)));
            }
        }

        public MainViewModel(AddSearchBarViewModel asbVM, AddTourManagementViewModel atmVM)
        {
            this.addSearchBarVM = asbVM;
            this.addTourManagementVM = atmVM;
            SearchCommand = new RelayCommand(param => Debug.Print(searchText));
        }


        public ICommand SearchCommand
        {
            get;
            
        }

    }
}
