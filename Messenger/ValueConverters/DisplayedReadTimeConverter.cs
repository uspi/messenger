using System;
using System.Globalization;
using System.Windows;

namespace Messenger
{
    /// <summary>
    /// Takes in date and converts it to a user friendly message read time
    /// </summary>
    class DisplayedReadTimeConverter : ValueConverter<DisplayedReadTimeConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // take the received time
            var time = (DateTimeOffset)value;

            // if it is not read
            if (time == DateTimeOffset.MinValue)
            {
                // show nothing 
                return string.Empty;
            }

            // if it is today
            if (time.Date == DateTimeOffset.UtcNow.Date)
            {
                // time (hours and minutes)
                return time.ToLocalTime().ToString("HH:mm");
            }

            // if not today, but the year is the same as now
            if (time.Year == DateTimeOffset.UtcNow.Year)
            {
                // date and time (day of month, name month, hours and minutes)
                return time.ToLocalTime().ToString("M MMMM, HH:mm");
            }

            // if it is not today, and another year full date 
            // (day of month, name month, year, hours and minutes)
            return time.ToLocalTime().ToString("M MMMM yyyy, HH:mm");
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
