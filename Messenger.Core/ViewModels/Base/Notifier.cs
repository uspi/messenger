using PropertyChanged;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Messenger.Core
{
    /// <summary>
    /// Base class for view model for property change notification. 
    /// Needed Fody.NuGet package for attribute work
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public class Notifier : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // name of the property is passed on the change of which you want to notify
        public void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
