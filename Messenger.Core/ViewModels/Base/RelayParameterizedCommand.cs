using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Messenger.Core
{
    public class RelayParameterizedCommand : ICommand
    {
        //private Action<object> _execute;
        //private Func<object, bool> _canExecute;

        //public event EventHandler CanExecuteChanged
        //{
        //    add => CommandManager.RequerySuggested += value; 
        //    remove => CommandManager.RequerySuggested -= value; 
        //}

        //public RelayCommand(Action<object> execute, 
        //    Func<object, bool> canExecute = null)
        //{
        //    _execute = execute;
        //    _canExecute = canExecute;
        //}

        //public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);

        //public void Execute(object parameter) => _execute(parameter);


        private Action<object> _Action;

        public event EventHandler CanExecuteChanged = (sender, e) => { };

        // constructor
        public RelayParameterizedCommand(Action<object> action) => _Action = action;

        // A relay command can always execute
        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter) => _Action(parameter);
    }
}

