using Messenger.Core;
using System;
using System.Windows;

namespace Messenger
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // start WPF application method
        protected override void OnStartup(StartupEventArgs e)
        {
            // let standard app startup method
            base.OnStartup(e);

            ApplicationSetup();

            // instead "startup uri" in MainWindow.xaml let's do the same with the code
            Current.MainWindow = new MainWindow();
            Current.MainWindow.Show();
        }

        // configures application
        private void ApplicationSetup()
        {
            // load IoC immediately before anything else
            IoC.Setup();
        }
    }
}
