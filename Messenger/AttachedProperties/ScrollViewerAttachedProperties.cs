using System.Windows;
using System.Windows.Controls;

namespace Messenger
{
    // scroll an items control to the bottom when the data context changes
    public class ScrollToBottomOnLoad : AttachedPropertyBase<ScrollToBottomOnLoad, bool> 
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(sender is ScrollViewer control))
            {
                return;
            }

            // focus (keyboard focus) this control when loaded
            control.ScrollChanged -= Control_ScrollChanged;
            control.ScrollChanged += Control_ScrollChanged;
        }

        private void Control_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            var scroll = sender as ScrollViewer;

            // if close enough to bottom
            if (scroll.ScrollableHeight - scroll.VerticalOffset < 10)
            {
                scroll.ScrollToEnd();
            }
        }
    }

    // auto keep scroll at the bottom of the screen when we are already close to the bottom
    public class ScrollToBottomAuto : AttachedPropertyBase<ScrollToBottomAuto, bool>
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(sender is ScrollViewer control))
            {
                return;
            }

            // focus (keyboard focus) this control when loaded
            control.DataContextChanged -= Control_DataContextChanged;
            control.DataContextChanged += Control_DataContextChanged;
        }

        private void Control_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            (sender as ScrollViewer).ScrollToBottom();
        }
    }
}
