using ControlzEx.Theming;
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
        public MainWindow()
        {
            InitializeComponent();
        }
        private void SwitchTheme_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            string theme = menuItem.Header.ToString();

            if (theme == "Light")
            {
                ThemeManager.Current.ChangeTheme(this, "Light.Blue");
            }
            else if (theme == "Dark")
            {
                ThemeManager.Current.ChangeTheme(this, "Dark.Blue");
            }
        }
    }
}
