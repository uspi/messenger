using Messenger.Core;
using Ninject;
using System;
using System.Diagnostics;
using System.Globalization;

namespace Messenger
{
    /// <summary>
    /// Converts a string name to a service pulled from the IoC container
    /// </summary>
    class IoCConverter : ValueConverter<IoCConverter>
    {
        public override object Convert(object value, Type targetType, 
            object parameter, CultureInfo culture)
        {
            
            // find the page you want
            switch ((string)parameter)
            {
                case nameof(ApplicationViewModel):
                    return IoC.Get<ApplicationViewModel>();

                default:
                    Debug.WriteLine("Existing page in page value converter");
                    Debugger.Break();
                    return null;
            }
        }

        public override object ConvertBack(object value, Type targetType, 
            object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
