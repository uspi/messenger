using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Messenger
{
    /// <summary>
    /// Helpers to animate pages in specific ways
    /// </summary>
    public static class PageAnimations
    {
        // slides a page in FROM the RIGHT
        public static async Task SlideAndFadeInFromRightAsync(this Page page, float seconds)
        {
            // create storyboard
            var sb = new Storyboard();

            // add slide from right animaiton
            sb.AddSlideFromRight(seconds, page.WindowWidth);
            
            // add fade in animation
            sb.AddFadeIn(seconds);

            // start animating
            sb.Begin(page);

            // make page visible
            page.Visibility = Visibility.Visible;

            // wait for animation to finish
            await Task.Delay((int)(seconds * 1000));
        }

        // slides a page out TO the LEFT
        public static async Task SlideAndFadeOutToLeftAsync(this Page page, float seconds)
        {
            // create storyboard
            var sb = new Storyboard();

            // add slide from right animaiton
            sb.AddSlideToLeft(seconds, page.WindowWidth);

            // add fade in animation
            sb.AddFadeOut(seconds);

            // start animating
            sb.Begin(page);

            // make page visible
            page.Visibility = Visibility.Visible;

            // wait for animation to finish
            await Task.Delay((int)(seconds * 1000));
        }
    }
}
