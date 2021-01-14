using Messenger.Core;
using System;
using System.Diagnostics;
using System.Globalization;

namespace Messenger
{
    /// <summary>
    /// Converts the <see cref="ApplicationPage"/> to an actual ViewPage
    /// </summary>
    public static class ApplicationPageHelpers
    {
        public static PageBase Convert(this ApplicationPage page, object viewModel = null)
        {
            // find the page you want
            switch (page)
            {
                case ApplicationPage.SignIn:
                    return new SignInPage(viewModel as SignInViewModel);

                case ApplicationPage.SignUp:
                    return new SignUpPage(viewModel as SignUpViewModel);

                case ApplicationPage.Chat:
                    return new ChatPage(viewModel as ChatMessageListViewModel);

                default:
                    Debug.WriteLine("Existing page in page value converter");
                    Debugger.Break();
                    return null;
            }
        }

        // convert PageBase to specific ApplicationPage
        public static ApplicationPage ToApplicationPage(this PageBase page)
        {
            if (page is ChatPage)
            {
                return ApplicationPage.Chat;
            }
            if (page is SignInPage)
            {
                return ApplicationPage.SignIn;
            }
            if (page is SignUpPage)
            {
                return ApplicationPage.SignUp;
            }
            Debug.WriteLine("Existing page in ToApplicationPage converter");
            Debugger.Break();
            return default(ApplicationPage);
        }
    }
}
