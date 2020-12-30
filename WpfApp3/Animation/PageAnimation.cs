namespace WPFClient
{
    /// <summary>
    /// Styles of page animations for appearing/disappearing
    /// </summary>
    public enum PageAnimation
    {
        // No animation takes place
        None = 0,

        // The page slides in and fades in, from the right
        SlideAndFadeInFromRight = 1,

        // The page slides in and fades out to the left 
        SlideAndFadeOutToLeft = 2
    }
}
