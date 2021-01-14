using System.Windows;
using System.Windows.Controls;

namespace Messenger
{
    // keyboard focus this element on load
    public class IsFocusedProperty : AttachedPropertyBase<IsFocusedProperty, bool> 
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            // don't have a control
            if (!(sender is Control control))
            {
                return;
            }

            // focus (keyboard focus) this control when loaded
            control.Loaded += (s, se) => control.Focus();
        }
    }
}
