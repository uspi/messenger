using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace WPFClient
{
    /// <summary>
    /// Animation helpers for <see cref="Storyboard"/>
    /// </summary>
    public static class StoryboardHelpers
    {
        // adds a slide from right animation to the storyboard
        public static void AddSlideFromRight(this Storyboard storyboard, float seconds, 
            double offset, float decelerationRatio = 0.9f)
        {
            // create the margin animate from right
            var animation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = new Thickness(offset, 0, -offset, 0),
                To = new Thickness(0),
                DecelerationRatio = decelerationRatio
            };

            // set the target property name
            Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));

            // add this to the storyboard
            storyboard.Children.Add(animation);
        }

        // adds a slide to left animation to the storyboard
        public static void AddSlideToLeft(this Storyboard storyboard, float seconds,
            double offset, float decelerationRatio = 0.9f)
        {
            // create the margin animate from right
            var animation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = new Thickness(0),
                To = new Thickness(-offset, 0, offset, 0),
                DecelerationRatio = decelerationRatio
            };

            // set the target property name
            Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));

            // add this to the storyboard
            storyboard.Children.Add(animation);
        }

        // adds a fade animation to the storyboard
        public static void AddFadeIn(this Storyboard storyboard, float seconds)
        {
            // create the margin animate from right
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
            // create the margin animate from right
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
    }
}
