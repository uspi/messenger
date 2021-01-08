using System;
using System.Diagnostics;
using System.Windows;

namespace Messenger
{
    /// <summary>
    /// Animation for any <see cref="FrameworkElement"/>. Base class to run any animation method
    /// when a <see cref="bool"/> is set to true
    /// and a reverse animation when set to false
    /// </summary>
    public abstract class AnimatePropertyBase<Parent> : AttachedPropertyBase<Parent, bool>
        where Parent : AttachedPropertyBase<Parent, bool>, new()
    {
        #region Public Properties

        // indicating if this is the first time property loaded
        public bool FirstLoad { get; set; } = true;

        #endregion
        public override void OnValueUpdated(DependencyObject sender, object value)
        {
            // we only work with Framework Elements
            if (!(sender is FrameworkElement element))
                // if true create variable FrameworkElement element
            {
                return;
            }

            // don't fire if the current value the same. but fire if it's first load
            if (sender.GetValue(ValueProperty) == value && !FirstLoad)
            {
                return;
            }

            // on first load hook to the element, 
            // wait when its load and run the code
            if (FirstLoad)
            {
                // single self-unhookable event for the
                // elements Loaded event
                RoutedEventHandler onLoaded = null;
                onLoaded = (ss, ee) =>
                {
                    // unhook ourselves
                    element.Loaded -= onLoaded;

                    // action code
                    // do disred animation
                    Animate(element, (bool)value);

                    // no longer in first load
                    FirstLoad = false;
                };

                // hook into the Loaded event of the element
                element.Loaded += onLoaded;    
            }
            else
            {
                // just start animate this element
                Animate(element, (bool)value);
            }
        }

        // start when the value changes
        protected virtual void Animate(FrameworkElement element, bool value) { }
    }

    /// <summary>
    /// Animates a <see cref="FrameworkElement"/> sliding it in from the left on show 
    /// and sliding out to the left on hide
    /// </summary>
    public class AnimateSlideInFromLeftProperty : AnimatePropertyBase<AnimateSlideInFromLeftProperty>
    {
        protected override async void Animate(FrameworkElement element, bool value)
        {
            // if true start animation in
            if (value)
            {
                await element.SlideAndFadeInFromLeftAsync(FirstLoad ? 0 : 0.3f, keepMargin: false);
            }
            // if false start animation out
            else
            {
                await element.SlideAndFadeOutToLeftAsync(FirstLoad ? 0 : 0.3f, keepMargin: false);
            }
        }
    }
}
