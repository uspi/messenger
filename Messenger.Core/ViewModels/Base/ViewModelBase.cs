using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Messenger.Core
{
    /// <summary>
    /// Base View Model for all in the application. Inherits class <see cref="Notifier"/> 
    /// which automatically implements interface <see cref="INotifyPropertyChanged"/> 
    /// for all public properties
    /// </summary>
    public class ViewModelBase : Notifier
    {
        /// <summary>
        /// Runs a command if the <paramref name="updatingFlag"/> is not set.
        /// If the <paramref name="updatingFlag"/> is <see cref="true"/> 
        /// (indicating the function is already running) then the action is not run.
        /// If the <paramref name="updatingFlag"/> is <see cref="false"/>
        /// (indicating no running function) then the action is run
        /// Once the action is finished if it was run, then the flag is reset to false once done
        /// </summary>
        /// <param name="updatingFlag"></param>
        /// <param name="action">action to run if the command is not already running</param>
        /// <returns></returns>
        protected async Task RunCommand(Expression<Func<bool>> updatingFlag, Func<Task> action)
        // Expression because we want the function to be executed in run time
        {
            // check if the flag property is true (function is already running)
            if (updatingFlag.GetPropertyValue())
            {
                return;
            }

            // set the property flag to true to indicate we are running
            updatingFlag.SetPropertyValue(true);

            try
            {
                // run the passed in action
                await action();
            }
            finally
            {
                // now it's finished
                updatingFlag.SetPropertyValue(false);
            }
        }
    }
}
