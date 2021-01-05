using Messenger.Core;
using System;
using System.Diagnostics;
using System.Globalization;

namespace Messenger
{
    /// <summary>
    /// Converts the <see cref="ApplicationPage"/> to an actual ViewPage
    /// </summary>
    class PageValueConverter : ValueConverter<PageValueConverter>
    {
        public override object Convert(object value, Type targetType, 
            object parameter, CultureInfo culture)
        {
            // find the page you want
            switch ((ApplicationPage)value)
            {
                case ApplicationPage.SignIn:
                    return new SignInPage();

                case ApplicationPage.SignUp:
                    return new SignUpPage();

                case ApplicationPage.Chat:
                    return new ChatPage();

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
