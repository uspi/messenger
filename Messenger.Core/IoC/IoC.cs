using Ninject;
using System;

namespace Messenger.Core
{
    // tool for our app
    public static class IoC
    {
        // kernel for our IoC container
        public static IKernel Kernel { get; private set; } = new StandardKernel();

        /// <summary>
        /// Sets up the IoC container, binds all info required and is ready
        /// for use.
        /// Must be called as soon as your app starts up to ensure 
        /// all services can be found
        /// </summary>
        public static void Setup()
        {
            // bind all required view models
            BindViewModels();
        }

        // binds all singleton view models
        private static void BindViewModels()
        {
            // bind to signle instance of ApplicationViewModel
            Kernel.Bind<ApplicationViewModel>().ToConstant(new ApplicationViewModel());
        }

        // get's a service form the IoC, of the specified type
        public static T Get<T>() => Kernel.Get<T>();
    }
}
