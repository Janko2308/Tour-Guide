using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Tour_Planner.ViewModels
{
    class BaseViewModel : INotifyPropertyChanged


    {
        public event PropertyChangedEventHandler? PropertyChanged;

        //Callermembername sets the propertyname automatically to the membername of the caller

        protected void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        
    }
}
