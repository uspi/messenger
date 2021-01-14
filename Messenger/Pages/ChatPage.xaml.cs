using Messenger.Core;
using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace Messenger
{
    /// <summary>
    /// Interaction logic for ChatPage.xaml
    /// </summary>
    public partial class ChatPage : PageBase<ChatMessageListViewModel>
    {
        public ChatPage()
        {
            InitializeComponent();
        }

        public ChatPage(ChatMessageListViewModel specificViewModel = null) : base(specificViewModel)
        {
            InitializeComponent();
        }

        protected override void OnViewModelChanged()
        {
            if (ChatMessageList == null) { return; }

            // fade in chat messages list
            var storyboard = new Storyboard();
            //storyboard.AddFadeIn(1);
            storyboard.Begin(ChatMessageList);

            // set focus to message box
            MessageText.Focus();
        }

        private void MessageText_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var textbox = sender as TextBox;

            // enter pressed
            if (e.Key == Key.Enter)
            {
                // check (ctrl + enter) or (shift + enter)
                if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control) || Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
                {
                    // add new line at the cursor point
                    var index = textbox.CaretIndex;

                    // insert new line
                    textbox.Text = textbox.Text.Insert(index, Environment.NewLine);

                    // shift caret forward to newline
                    textbox.CaretIndex = index + Environment.NewLine.Length;

                    // key as hadled by us
                    e.Handled = true;
                }
                else
                {
                    // send message
                    ViewModel.Send();
                }

                // key as hadled by us
                e.Handled = true;
            }
        }
    }
}
