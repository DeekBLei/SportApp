using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModelService
{
    public class Command : INotifyPropertyChanged, ICommand
    {
        public event EventHandler CanExecuteChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        private Action methodToExecute = null;
        private Func<bool> methodToDetectCanExecute = null;


        public Command(Action methodToExecute, Func<bool> methodToDetectCanExecute)
        {
            this.methodToExecute = methodToExecute;
            this.methodToDetectCanExecute = methodToDetectCanExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (this.methodToDetectCanExecute == null)
            {
                return true;
            }
            else
            {

                return this.methodToDetectCanExecute();
            }
        }
        public void TriggerCanExecuteChanged()
        {
            if (this.CanExecuteChanged != null)
            {
                this.CanExecuteChanged(this, EventArgs.Empty);
                this.OnPropertyChanged(nameof(IsVisible));
            }
        }
       
        public void TriggerCanExecuteChanged(object sender, PropertyChangedEventArgs e)
                {
            if (this.CanExecuteChanged != null)
            {
                this.CanExecuteChanged(this, EventArgs.Empty);
                this.OnPropertyChanged(nameof(IsVisible));
            }
        }

        public string IsVisible
        {
            get
            {
                if (this.methodToDetectCanExecute == null)
                {
                    return "Visible";
                }
                else
                {

                    if (this.methodToDetectCanExecute())
                    {
                        return "Visible";
                    }
                    else
                    {
                        return "Collapsed";
                    }
                }
            }
        }

        public void Execute(object parameter)
        {
            this.methodToExecute();
        }
        protected virtual void OnPropertyChanged(string propertyName)
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
