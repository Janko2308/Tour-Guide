using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using Tour_Planner.BL;
using Tour_Planner.DAL;
using Tour_Planner.ViewModels;

namespace Tour_Planner.Views {
    /// <summary>
    /// Interaction logic for AddNewTour.xaml
    /// </summary>
    public partial class AddNewTour : Window {
        public AddNewTour() {
            InitializeComponent();
            DataContext = new AddNewTourViewModel(new TourManager(new DataManagerEntityFrameworkImpl()));
        }
    }
}
