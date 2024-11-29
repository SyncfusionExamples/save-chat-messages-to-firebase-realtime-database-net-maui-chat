using Firebase.Database.Offline;
using Syncfusion.Maui.Chat;
using Syncfusion.Maui.Core;
using Syncfusion.Maui.Popup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatMaui
{
    public class PageBehavior : Behavior<ContentPage>
    {
        private SfChat chat;
        ViewModel viewModel;

        public IDisposable SuscribeInGrupalChat;
        protected override void OnAttachedTo(ContentPage bindable)
        {
            viewModel = new ViewModel();

            chat = bindable.FindByName<SfChat>("sfChat");
            bindable.BindingContext = viewModel;

            chat.SendMessage += OnSendMessage;

            bindable.Appearing += OnAppearing;
            bindable.Disappearing += OnDisappearing;

            base.OnAttachedTo(bindable);
        }

        private void OnDisappearing(object? sender, EventArgs e)
        {
            this.SuscribeInGrupalChat.Dispose();
        }

        private void OnAppearing(object? sender, EventArgs e)
        {
            this.SuscribeInGrupalChat = viewModel.LoadMessages();
        }

        private async void OnSendMessage(object? sender, SendMessageEventArgs e)
        {
            // Set Handled=True, To prevent adding the new message automatically and enables user to add it manually.
            e.Handled = true;

            var newMessage = e.Message?.Text;
            
            var dt = DateTime.Now;

            chat.Editor.Text = string.Empty;

            if (!string.IsNullOrEmpty(newMessage) && !string.IsNullOrEmpty(viewModel.CurrentUser.Name))
            {
                await viewModel.FireBaseDatabase.SendMessageAsync(new ChatMessage
                {                    
                    Author = viewModel.CurrentUser,
                    Content = newMessage,
                    Timestamp = dt,
                });
            }
        }

        protected override void OnDetachingFrom(ContentPage bindable)
        {
            chat.SendMessage -= OnSendMessage;
            bindable.Appearing -= OnAppearing;
            bindable.Disappearing -= OnDisappearing;
            chat = null;
            viewModel = null;
            base.OnDetachingFrom(bindable);
        }
    }
}
