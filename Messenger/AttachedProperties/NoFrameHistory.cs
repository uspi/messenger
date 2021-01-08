using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Messenger
{
    /// <summary>
    /// Creatin a <see cref="Frame"/> that never shows navigation
    /// and keeps the navigation history empty
    /// </summary>
    public class NoFrameHistory : AttachedPropertyBase<NoFrameHistory, bool> 
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            // get frame from sender
            var frame = sender as Frame;

            // hide line navigation bar
            frame.NavigationUIVisibility = NavigationUIVisibility.Hidden;

            // clear history(remove back entry) on navigate
            frame.Navigated += (ss, ee) => ((Frame)ss).NavigationService.RemoveBackEntry();
        }
    }
}
