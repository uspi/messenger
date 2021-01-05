using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Messenger
{
    /// <summary>
    /// Allows you to add converter code directly to the code without 
    /// adding to the resource file.
    /// Can be used directly in the XAML like {StaticResource}.
    /// </summary>
    /// <typeparam name="T">Represents the type of converter</typeparam>
    public abstract class ValueConverter<T> : MarkupExtension, IValueConverter
        where T : class, new()
    {
        private static T converter = null;

        // provides a static instance ot the value converter 
        public override object ProvideValue(IServiceProvider serviceProvider) 
            => converter ?? (converter = new T());

        // convert to type.
        public abstract object Convert(object value, Type targetType, 
            object parameter, CultureInfo culture);

        // convert to origin type
        public abstract object ConvertBack(object value, Type targetType, 
            object parameter, CultureInfo culture);
    }
}
