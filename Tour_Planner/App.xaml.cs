using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Tour_Planner.ViewModels;
using Tour_Planner.Views;

namespace Tour_Planner
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // create all viewmodels (and inject them later)
            var addSearchBarModel = new AddSearchBarViewModel();
            var addTourManagementViewModel = new AddTourManagementViewModel();


            var wnd = new MainWindow
            {
                DataContext = new MainViewModel(addSearchBarModel, addTourManagementViewModel),
                AddSearchBar = { DataContext = addSearchBarModel },
                AddTourManagement = { DataContext = addTourManagementViewModel }
            };
            wnd.Show();
        }
    }
}
