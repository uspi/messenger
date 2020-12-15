﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPFClient
{
    /// <summary>
    /// Base class for view model for property change notification 
    /// </summary>
    public class Notifier : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // name of the property is passed on the change of which you want to notify
        public void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}