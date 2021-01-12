using System;
using System.Globalization;
using System.Windows;

namespace Messenger
{
    /// <summary>
    /// A converter that takes in a boolean and returns a <see cref="Visibility"/>
    /// the difference from a <see cref="BooleanToVisibilityConverter"/> is that 
    /// this one translates <see cref="false"/> into <see cref="Visibility.Collapsed"/> 
    /// </summary>
    class BooleanToVisibilityCollapsedConverter : ValueConverter<BooleanToVisibilityCollapsedConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null)
            {
                return (bool)value ? Visibility.Collapsed : Visibility.Visible;
            }
            else
            {
                // inverting before values
                return (bool)value ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
