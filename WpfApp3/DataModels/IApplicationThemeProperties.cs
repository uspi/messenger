using System.Windows.Media;

namespace WPFClient
{
    /// <summary>
    /// Defines properties common to all visual themes of the application.
    /// Contains elements whose settings are easier to synchronize in code than in XAML.
    /// </summary>
    public interface IApplicationThemeProperties
    {
        SolidColorBrush HeadLine { get; }

        #region HeadLine Buttons
        // state non-active
        SolidColorBrush MinimizeButton { get; }
        SolidColorBrush ExpandButton { get; }
        SolidColorBrush CloseButton { get; }

        // state active
        SolidColorBrush MinimizeButton_Active { get; }
        SolidColorBrush ExpandButton_Active { get; }
        SolidColorBrush CloseButton_Active { get; }
        #endregion

        SolidColorBrush ContentBody { get; }
    }
}
