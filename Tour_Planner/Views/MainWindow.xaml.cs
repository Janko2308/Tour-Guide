using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tour_Planner.Model;

namespace Tour_Planner.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TourItem selectedTour = new();

        public TourItem SelectedTour {
            get => selectedTour;
            set {
                selectedTour = value;
                Console.WriteLine("Changing");
            }
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        public void ChangeSelected(object sender, SelectionChangedEventArgs e) {
            if (ToursView.SelectedItem == null) {
                return;
            }
            SelectedTour = ToursView.SelectedItem as TourItem;
        }
    }
}
