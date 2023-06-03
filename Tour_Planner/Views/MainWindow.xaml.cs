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
using Tour_Planner.ViewModels;

namespace Tour_Planner.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public App CurrentApplication { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }



        private void ChangeTheme(object sender, RoutedEventArgs e) {
            switch (int.Parse(((MenuItem)sender).Uid)) {
                //RedBlackTheme
                case 0: ThemesController.SetTheme(ThemeType.RedBlack); break;
                //DeepDarkTheme
                case 1: ThemesController.SetTheme(ThemeType.DeepDark); break;
                //DeepDarkTheme
                case 2: ThemesController.SetTheme(ThemeType.SoftDark); break;
            }
            e.Handled = true;
        }

    }
}
