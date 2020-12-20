using System.Windows.Media;

namespace WPFClient
{
    /// <summary>
    /// Defines properties common to all visual themes of the application.
    /// Contains elements whose settings are easier to synchronize in code than in XAML.
    /// </summary>
    public interface IApplicationThemeProperties
    {
        Color HeadLine { get; }
        Color ContentBody { get; }

        #region HeadLine Buttons
        // state non-active
        Color MinimizeButton { get; }
        Color MaximazeButton { get; }
        Color CloseButton { get; }

        // state active
        Color MinimizeButton_Active { get; }
        Color MaximazeButton_Active { get; }
        Color CloseButton_Active { get; }
        #endregion   
    }
}
