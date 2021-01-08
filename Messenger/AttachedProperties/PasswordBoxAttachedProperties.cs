using System;
using System.Windows;
using System.Windows.Controls;

namespace Messenger
{
    // monitor password box changes
    public class MonitorPasswordProperty : AttachedPropertyBase<MonitorPasswordProperty, bool> 
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            // get the caller
            PasswordBox passwordBox = sender as PasswordBox;

            // make sure it is a password box
            if (passwordBox == null)
            {
                return;
            }

            // remuve any previus events
            passwordBox.PasswordChanged -= PasswordBox_PasswordChanged;

            // if the caller set MonitorPassword to true...
            if ((bool)e.NewValue)
            {
                // set default value
                HasTextProperty.SetValue(passwordBox);

                // start listening out for password changes
                passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
            }
        }

        // Fired when the password box password value changes
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        { 
            // set the attached HasText value 
            HasTextProperty.SetValue((PasswordBox)sender);
        }
    }

    // checks password box for a value
    public class HasTextProperty : AttachedPropertyBase<HasTextProperty, bool> 
    {
        // Sets the HasText property based on if the caller PasswordBox has any text
        public static void SetValue(DependencyObject sender)
        {
            SetValue(sender, ((PasswordBox)sender).SecurePassword.Length > 0);
        }
    }
}
