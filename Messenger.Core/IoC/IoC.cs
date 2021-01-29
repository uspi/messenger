using Ninject;
using System;
using System.Threading;

namespace Messenger.Core
{
    // tool for our app
    public static class IoC
    {
        // kernel for our IoC container
        public static IKernel Kernel { get; private set; } = new StandardKernel();

        public static ApplicationViewModel Application { get => Get<ApplicationViewModel>(); }

        public static SignInViewModel SignInViewModel { get => Get<SignInViewModel>(); }

        public static SignUpViewModel SignUpViewModel { get => Get<SignUpViewModel>(); }

        public static NetworkConnection NetworkConnection { get => Get<NetworkConnection>(); }
        
        public static ChatListViewModel ChatListViewModel { get => Get<ChatListViewModel>(); }

        public static ChatListDesignModel ChatListDesignModel { get => Get<ChatListDesignModel>(); }

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

            // bind to network connection
            Kernel.Bind<NetworkConnection>().ToConstant(new NetworkConnection());
        }

        // binds all singleton view models
        private static void BindViewModels()
        {
            // bind to signle instance of ApplicationViewModel
            Kernel.Bind<ApplicationViewModel>().ToConstant(new ApplicationViewModel());

            Kernel.Bind<SignInViewModel>().ToConstant(new SignInViewModel());

            Kernel.Bind<SignUpViewModel>().ToConstant(new SignUpViewModel());

            Kernel.Bind<ChatListViewModel>().ToConstant(new ChatListViewModel());

            Kernel.Bind<ChatListDesignModel>().ToConstant(new ChatListDesignModel());
        }

        // get's a service form the IoC, of the specified type
        public static T Get<T>() => Kernel.Get<T>();
    }
}
