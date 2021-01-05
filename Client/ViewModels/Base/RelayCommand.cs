using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPFClient
{
    class RelayCommand : ICommand
    {
        private Action mAction;

        public event EventHandler CanExecuteChanged = (sender, e) => { };

        public RelayCommand(Action action) => mAction = action;

        // A relay command can always execute
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter) => mAction();
    }
}

