using System;
using System.Windows;

namespace WPFClient
{
    /// <summary>
    /// A base attached property to replace the default wpf attached property
    /// </summary>
    /// <typeparam name="Parent">parent class to be attached property</typeparam>
    /// <typeparam name="Property">type of this attached property</typeparam>
    public abstract class AttachedProperty<Parent, Property> 
        where Parent : new()
    {
        #region Public Events
        /// <summary>
        /// Fired when the value changes
        /// </summary>
        public event Action<DependencyObject, DependencyPropertyChangedEventArgs> ValueChanged = (sender, e) => { };
        #endregion

        #region Public Properties
        // instance of our parent class
        public static Parent Instance { get; private set; } = new Parent();
        #endregion

        #region Attached Property Definitions
        /// <summary>
        /// The attached property for this class
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.RegisterAttached(
                "Value",
                typeof(Property),
                typeof(AttachedProperty<Parent, Property>),
                new PropertyMetadata(
                    new PropertyChangedCallback(OnValuePropertyChanged)
                    ));
        /// <summary>
        /// Callback event when the <see cref="ValueProperty"/> is changed
        /// </summary>
        /// <param name="d">UI element that had its property changed</param>
        /// <param name="e">Argument for the event</param>
        private static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // call the parent function
            Instance.OnValueChanged(d, e);

            // call event listenters
            Instance.ValueChanged(d, e);
        }

        /// <summary>
        /// Gets the attached property
        /// </summary>
        ///<param name="d">element to get the property from</param>
        public static Property GetValue(DependencyObject d) => (Property)d.GetValue(ValueProperty);

        /// <summary>
        /// Sets the attached property
        /// </summary>
        /// <param name="d">element to get the property from</param>
        /// <param name="value">value to set the property to</param>
        public static void SetValue(DependencyObject d, Property value) => d.SetValue(ValueProperty, value);
        #endregion

        #region Event Methods
        /// <summary>
        /// The method that is called when any attached property of this type is changed
        /// </summary>
        /// <param name="sender">The UI element that this poperty was changed for</param>
        /// <param name="e">The arguments for this events</param>
        public virtual void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) { }
        #endregion
    }
}
