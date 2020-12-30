using System.ComponentModel;

namespace WPFClient
{
    /// <summary>
    /// Base View Model for all in the application. Inherits class <see cref="Notifier"/> 
    /// which automatically implements interface <see cref="INotifyPropertyChanged"/> 
    /// for all public properties
    /// </summary>
    public class BaseViewModel : Notifier
    {
        // version of the windows operating system
        internal CurrentOS _CurrentOS { get; } = new CurrentOS();

        // start page for the application
        public ApplicationPage CurrentPage { get; set; } = ApplicationPage.Login;
    }
}
