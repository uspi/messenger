using Messenger.Core;

namespace Messenger
{
    /// <summary>
    /// Locates view models from the IoC for use in binding in XAML files
    /// </summary>
    public class ViewModelLocator
    {
        // singleton instance of the locator
        public static ViewModelLocator Instance { get; private set; } = new ViewModelLocator();

        public static ApplicationViewModel ApplicationViewModel 
        {
            get => IoC.Get<ApplicationViewModel>();
        }
    }
}
