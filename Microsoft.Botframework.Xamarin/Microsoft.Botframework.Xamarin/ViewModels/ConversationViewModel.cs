using FreshMvvm;
using Microsoft.Bot.Connector.DirectLine;
using Microsoft.Botframework.Xamarin.Contract;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Microsoft.Botframework.Xamarin.ViewModels
{
    public class ConversationViewModel : FreshBasePageModel
    {
        private IBotService _botService;
        private string _conversationId;
        private ICommand _sendMessageCommand;
        private string _message;
        private bool _isSendButtonEnabled;
        private Activity PreviousSentActivity { get; set; } = new Activity();

        public ObservableCollection<Activity> Messages { get; set; } = new ObservableCollection<Activity>();
        public ICommand SendMessageCommand => _sendMessageCommand ?? (_sendMessageCommand = new Command(async (o) => await OnSendMessage(o)));
        public bool IsSendButtonEnabled
        {
            get { return _isSendButtonEnabled; }
            set { _isSendButtonEnabled = value; RaisePropertyChanged("IsSendButtonEnabled"); }
        }
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                RaisePropertyChanged("Message");
                if (_message != string.Empty)
                {
                    IsSendButtonEnabled = true;
                }
                else
                {
                    IsSendButtonEnabled = false;
                }
            }
        }

        public ConversationViewModel(IBotService botService)
        {
            _botService = botService;
        }

        public override void Init(object initData)
        {
            Task.Factory.StartNew(async () => _conversationId = await _botService.StartConversation());
            _botService.ActivityReceived += ActivityReceived;
            _botService.ActivitySent += ActivitySent;
            base.Init(initData);
        }

        private void ActivitySent(object sender, ActivityEventArgs e)
        {
            PreviousSentActivity = e.Activity;
            Messages.Add(PreviousSentActivity);
        }

        private void ActivityReceived(object sender, ActivityEventArgs e)
        {
            if (string.Equals(e.Activity.Type, "message")) // only add messages to our list
            {
                Messages.Add(e.Activity);
            }
        }

        public async Task OnSendMessage(object obj, bool isAttachmentIncluded = false)
        {
            try
            {
                var previousMessage = Message;
                // Clear entry field after sending
                Message = string.Empty;

                var message = await _botService.SendMessage(previousMessage);

                if (message != null || message.Id != null || message.Id != "")
                {
                    var indexOfSentMessage = Messages.IndexOf(PreviousSentActivity);
                    var prevSentActivity = PreviousSentActivity;

                    prevSentActivity.Id = message.Id;
                    Messages.Insert(indexOfSentMessage, prevSentActivity);
                    Messages.Remove(PreviousSentActivity);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
