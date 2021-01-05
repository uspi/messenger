using System;
using System.Globalization;
using System.Windows.Media;

namespace Messenger
{
    /// <summary>
    /// A converter that takes in an RGB string like f03f0f and converts it to a wpf <see cref="SolidColorBrush"/>
    /// </summary>
    class StringRGBToBrushConverter : ValueConverter<StringRGBToBrushConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (SolidColorBrush)new BrushConverter().ConvertFrom($"#{value}");
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
