using System;
using System.Windows;

namespace Messenger
{
    /// <summary>
    /// A base attached property to replace the default wpf attached property
    /// </summary>
    /// <typeparam name="Parent">parent class to be attached property</typeparam>
    /// <typeparam name="Property">type of this attached property</typeparam>
    public abstract class AttachedPropertyBase<Parent, Property> 
        where Parent : new()
    {
        #region Public Events
        /// <summary>
        /// Fired when the value changes
        /// </summary>
        public event Action<DependencyObject, DependencyPropertyChangedEventArgs> 
            ValueChanged = (sender, e) => { };

        /// <summary>
        /// Fired when the value changes even if it is the same value
        /// </summary>
        public event Action<DependencyObject, object> 
            ValueUpdated = (sender, value) => { };
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
            DependencyProperty.RegisterAttached
            (
                name: "Value",
                propertyType: typeof(Property),
                ownerType: typeof(AttachedPropertyBase<Parent, Property>),
                defaultMetadata: new PropertyMetadata(
                    default(Property),

                    // will work, if you change true with false or false with true
                    new PropertyChangedCallback(OnValuePropertyChanged),

                    // will work, if you set the value even equal to the previous one
                    new CoerceValueCallback(OnValuePropertyUpdated))
            );

        /// <summary>
        /// Callback event when the <see cref="ValueProperty"/> is changed
        /// </summary>
        /// <param name="d">UI element that had its property changed</param>
        /// <param name="e">Argument for the event</param>
        private static void OnValuePropertyChanged(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // call the parent function
            (Instance as AttachedPropertyBase<Parent, Property>)?.OnValueChanged(d, e);

            // call event listenters
            (Instance as AttachedPropertyBase<Parent, Property>)?.ValueChanged(d, e);      
        }

        /// <summary>
        /// Callback event when the <see cref="ValueProperty"/> is changed
        /// even if it is the same value
        /// </summary>
        /// <param name="d">UI element that had its property changed</param>
        /// <param name="e">Argument for the event</param>
        private static object OnValuePropertyUpdated(
            DependencyObject d, object value)
        {
            // call the parent function
            (Instance as AttachedPropertyBase<Parent, Property>)?.OnValueUpdated(d, value);

            // call event listenters
            (Instance as AttachedPropertyBase<Parent, Property>)?.ValueUpdated(d, value);

            // return original value
            return value;
        }

        /// <summary>
        /// Gets the attached property
        /// </summary>
        ///<param name="d">element to get the property from</param>
        public static Property GetValue(DependencyObject d) => 
            (Property)d.GetValue(ValueProperty);

        /// <summary>
        /// Sets the attached property
        /// </summary>
        /// <param name="d">element to get the property from</param>
        /// <param name="value">value to set the property to</param>
        public static void SetValue(DependencyObject d, Property value) => 
            d.SetValue(ValueProperty, value);

        #endregion

        #region Event Methods
        /// <summary>
        /// The method that is called when any attached property of this type is changed
        /// </summary>
        /// <param name="sender">The UI element that this poperty was changed for</param>
        /// <param name="e">The arguments for this events</param>
        public virtual void OnValueChanged(
            DependencyObject sender, DependencyPropertyChangedEventArgs e) { }

        /// <summary>
        /// The method that is called when any attached property of this type is changed
        /// even if it is the same value
        /// </summary>
        /// <param name="sender">The UI element that this poperty was changed for</param>
        /// <param name="e">The arguments for this events</param>
        public virtual void OnValueUpdated(
            DependencyObject sender, object value) { }
        #endregion
    }
}
