﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        private ObservableCollection<TourItem> filteredTours;
        private ObservableCollection<TourItem> allTours;

        public ObservableCollection<TourItem> Tours { get; set; }
        public ObservableCollection<TourLogs> TourLogs { get; set; }

        public TourItem SelectedTour {
            get => selectedTour;
            set {
                selectedTour = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedTour)));
            }
        }

        private TourLogs selectedTourLog;

        public TourLogs SelectedTourLog {
            get => selectedTourLog;
            set {
                selectedTourLog = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedTourLog)));
            }
        }

        public ObservableCollection<TourItem> FilteredTours { 
            get => filteredTours;
            set { 
                filteredTours = value;
            }
        }

        private void OnSearchRequested(object sender, string searchText)
        {
            this.Tours = this.allTours;
            ObservableCollection<TourItem> filteredTours = new ObservableCollection<TourItem>(this.Tours.Where(tour => tour.Name.Contains(searchText)));
            if(filteredTours.Count != 0) {
                this.Tours = filteredTours;
            }
            OnPropertyChanged(nameof(Tours));
        }

        public MainViewModel(AddSearchBarViewModel asbVM, AddNewTourViewModel antVM, TourManager bl) {
            this.addSearchBarVM = asbVM;
            this.addNewTourVM = antVM;
            this.Tours = new ObservableCollection<TourItem>(bl.GetTours());
            this.allTours = new ObservableCollection<TourItem>(bl.GetTours());
            this.TourLogs = new ObservableCollection<TourLogs>(bl.GetTourLogs());
            this.addSearchBarVM.SearchRequested += OnSearchRequested;
            // this.selectedTour is equal if this.Tours contain no entries then empty, else this.Tours[0]
            this.selectedTour = this.Tours.FirstOrDefault();
            this.selectedTourLog = this.TourLogs.FirstOrDefault();

            ExecuteCommandOpenNewTour = new RelayCommand(param => new Views.AddNewTour().ShowDialog());
            
            ExecuteCommandOpenNewTourLog = new RelayCommand(param => {
                try {
                    if (selectedTour == null) {
                        MessageBox.Show("Please select a tour first");
                    } else {
                        new Views.AddNewTourLog(selectedTour.Id).ShowDialog();
                    }
                }
                catch(Exception e) {
                    MessageBox.Show(e.Message);
                }
            });
            
            ExecuteCommandOpenEditTour = new RelayCommand(param => {
                try {
                    if (selectedTour == null) {
                        MessageBox.Show("Please select a tour to edit");
                    } else {
                        new Views.AddNewTour(selectedTour).ShowDialog();
                    }
                }
                catch(Exception e) {
                    MessageBox.Show(e.Message);
                }
            });

            ExecuteCommandOpenEditTourLog = new RelayCommand(param => {
                try {
                    if (selectedTourLog == null) {
                        MessageBox.Show("No tour log selected!");
                    } else {
                        new Views.AddNewTourLog(SelectedTourLog).ShowDialog();
                    }
                }
                catch (Exception e) {
                    MessageBox.Show(e.Message);
                }
            });
            
            ExecuteCommandDeleteThisTour = new RelayCommand(param => {
                try {
                    if (selectedTour == null) {
                        MessageBox.Show("Please select a tour to delete");
                        return;
                    }

                    var yesOrNo = MessageBox.Show($"Do you really want to delete Tour '{SelectedTour.Name}'?", "Confirmation", MessageBoxButton.YesNo);

                    if (yesOrNo == MessageBoxResult.Yes) {
                        bl.DeleteTour(SelectedTour);
                        SelectedTour = Tours.FirstOrDefault();
                    }
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                }
            });

            ExecuteCommandDeleteThisTourLog = new RelayCommand(param => {
                try {
                    if (selectedTourLog == null) {
                        MessageBox.Show("No tour log selected!");
                        return;
                    }

                    var yesOrNo = MessageBox.Show($"Do you really want to delete this tour log? Comment name: {selectedTourLog.Comment}", "Confirmation", MessageBoxButton.YesNo);

                    if (yesOrNo == MessageBoxResult.Yes) {
                        bl.DeleteTourLog(SelectedTourLog);
                        SelectedTourLog = TourLogs.FirstOrDefault();
                    }
                }
                catch (Exception e) {
                    MessageBox.Show(e.Message);
                }
            });

            TemporaryButton = new RelayCommand(param => {
                Tours.Clear();
                selectedTour = null;
                TourLogs.Clear();
                this.Tours = new ObservableCollection<TourItem>(bl.GetTours());
                this.TourLogs = new ObservableCollection<TourLogs>(bl.GetTourLogs());
                this.selectedTour = this.Tours.FirstOrDefault();
            });
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public ICommand ExecuteCommandOpenNewTour { get; }
        public ICommand ExecuteCommandOpenNewTourLog { get; }
        public ICommand ExecuteCommandOpenEditTour { get; }
        public ICommand ExecuteCommandOpenEditTourLog { get; }
        public ICommand ExecuteCommandDeleteThisTour { get; }
        public ICommand ExecuteCommandDeleteThisTourLog { get; }
        public ICommand TemporaryButton { get; }

        

    }
}
