using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace Messenger
{
    /// <summary>
    /// Animation helpers for <see cref="Storyboard"/>
    /// </summary>
    public static class StoryboardHelpers
    {
        #region Slide

        /// adds a slide from right animation to the storyboard
        /// <param name="keepMargin">Whether to keep the element at the same
        /// width during animation</param>
        public static void AddSlideFromRight(this Storyboard storyboard, float seconds, 
            double offset, float decelerationRatio = 0.9f, bool keepMargin = true)
        {
            // create the animation 
            var animation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = new Thickness(left: keepMargin ? offset : 0, 
                                     top: 0, 
                                     right: -offset, 
                                     bottom: 0),
                To = new Thickness(0),
                DecelerationRatio = decelerationRatio
            };

            // set the target property name
            Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));

            // add this to the storyboard
            storyboard.Children.Add(animation);
        }

        /// adds a slide from left animation to the storyboard
        /// <param name="keepMargin">Whether to keep the element at the same
        /// width during animation</param>
        public static void AddSlideFromLeft(this Storyboard storyboard, float seconds,
            double offset, float decelerationRatio = 0.9f, bool keepMargin = true)
        {
            // create the animation 
            var animation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = new Thickness(left: -offset,
                                     top: 0,
                                     right: keepMargin ? offset : 0,
                                     bottom: 0),
                To = new Thickness(0),
                DecelerationRatio = decelerationRatio
            };

            // set the target property name
            Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));

            // add this to the storyboard
            storyboard.Children.Add(animation);
        }

        /// adds a slide to left animation to the storyboard
        /// <param name="keepMargin">Whether to keep the element at the same
        /// width during animation</param>
        public static void AddSlideToLeft(this Storyboard storyboard, float seconds,
            double offset, float decelerationRatio = 0.9f, bool keepMargin = true)
        {
            // create the animation 
            var animation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = new Thickness(0),
                To = new Thickness(left: -offset,
                                   top: 0,
                                   right: keepMargin ? offset : 0,
                                   bottom: 0),
                DecelerationRatio = decelerationRatio
            };

            // set the target property name
            Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));

            // add this to the storyboard
            storyboard.Children.Add(animation);
        }

        /// adds a slide to right animation to the storyboard
        /// <param name="keepMargin">Whether to keep the element at the same
        /// width during animation</param>
        public static void AddSlideToRight(this Storyboard storyboard, float seconds,
            double offset, float decelerationRatio = 0.9f, bool keepMargin = true)
        {
            // create the animation
            var animation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = new Thickness(0),
                To = new Thickness(left: keepMargin ? offset : 0,
                                   top: 0,
                                   right: -offset,
                                   bottom: 0),
                DecelerationRatio = decelerationRatio
            };

            // set the target property name
            Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));

            // add this to the storyboard
            storyboard.Children.Add(animation);
        }

        #endregion

        #region Fade

        // adds a fade animation to the storyboard
        public static void AddFadeIn(this Storyboard storyboard, float seconds)
        {
            // create the animation 
            var animation = new DoubleAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = 0,
                To = 1
            };

            // set the target property name
            Storyboard.SetTargetProperty(animation, new PropertyPath("Opacity"));

            // add this to the storyboard
            storyboard.Children.Add(animation);
        }

        // adds a fade out animation to the storyboard
        public static void AddFadeOut(this Storyboard storyboard, float seconds)
        {
            // create the animation 
            var animation = new DoubleAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = 1,
                To = 0
            };

            // set the target property name
            Storyboard.SetTargetProperty(animation, new PropertyPath("Opacity"));

            // add this to the storyboard
            storyboard.Children.Add(animation);
        }

        #endregion
    }
}
