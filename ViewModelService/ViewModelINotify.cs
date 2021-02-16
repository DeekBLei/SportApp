using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using System.ComponentModel;
using DataTypes;
using DataBaseService;
using Windows.UI.Xaml;
using System.Collections.Specialized;

/* Basis voor alle Viewmodels zodat INotifyPropertyChanged
 * overal geerfd word*/

namespace ViewModelService
{
    public class ViewModelINotify : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        

        protected void OnPropertyChanged(string propertyName)
        {
           
                    if (PropertyChanged != null)
                    {
#pragma warning disable IDE1005 // Delegate invocation can be simplified.
                        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
#pragma warning restore IDE1005 // Delegate invocation can be simplified.
                    }
              
        }
        
     


    }
}
