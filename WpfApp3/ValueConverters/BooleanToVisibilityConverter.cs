using System;
using System.Globalization;
using System.Windows;

namespace WPFClient
{
    /// <summary>
    /// A converter that takes in a boolean and returns a <see cref="Visibility"/>
    /// </summary>
    class BooleanToVisibilityConverter : ValueConverter<BooleanToVisibilityConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? Visibility.Hidden : Visibility.Visible;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
