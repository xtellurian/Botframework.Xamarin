using AdaptiveCards.Rendering;
using Microsoft.Bot.Connector.DirectLine;
using Microsoft.Botframework.Xamarin.Contract;
using Microsoft.Botframework.Xamarin.CustomViews;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Microsoft.Botframework.Xamarin.Implementations
{
    public class BotService : IBotService
    {
        private DirectLineClient _client;
        private string _directLineSecret = ""; // your secret here
        private Conversation _defaultConversation;
        private string _watermark;

        public event EventHandler<ActivityEventArgs> ActivitySent;
        public event EventHandler<ActivityEventArgs> ActivityReceived;

        public BotService()
        {
            if (string.IsNullOrEmpty(this._directLineSecret)) throw new ArgumentNullException("Direct Line Secret is required");

            _client = new DirectLineClient(_directLineSecret);
            MessagingCenter.Subscribe<AdaptiveCardLayout, ActionEventArgs>(this, "AdaptiveCardAction", _handleAdaptiveCardAction);

        }

        public async Task<string> StartConversation(bool isDefault = true)
        {
            var conversation = await _client.Conversations.StartConversationAsync();
            PollForMessages(conversation);
            if(isDefault || _defaultConversation == null)
            {
                _defaultConversation = conversation;
            }
            return conversation.ConversationId;
        }

        private void PollForMessages(Conversation conversation)
        {
            Device.StartTimer(new TimeSpan(0, 0, 1), () =>
              {
                  var activitySet =_client.Conversations.GetActivities(conversation.ConversationId, _watermark);
                  var activities = activitySet?.Activities.Where(_ => _.From.Id != GetUserId());
                  _watermark = activitySet.Watermark;
                  if (activities != null)
                  {
                      foreach (var activity in activities)
                      {
                          ActivityReceived?.Invoke(this, new ActivityEventArgs() { Activity = activity });
                      }
                  }
                  return true;
              });
        }

        public Activity SentActivity { get; set; }
        public async Task<ResourceResponse> SendMessage( string message, string conversationId = null)
        {
            if (conversationId == null)
            {
                conversationId = GetDefaultConversationId();
            }
            SentActivity = new Activity
            {
                From = GetChannelAccount(),
                Text = message,
                Type = ActivityTypes.Message
            };

            ActivitySent?.Invoke(this, new ActivityEventArgs() { Activity = SentActivity });

            var response = await _client.Conversations.PostActivityAsync(conversationId, SentActivity);
            return response;
        }

        private ChannelAccount GetChannelAccount()
        {
            return new ChannelAccount(GetUserId());
        }

        private string GetDefaultConversationId()
        {
            return _defaultConversation.ConversationId ?? throw new NullReferenceException("Default Conversation was not set");
        }

        private string GetUserId()
        {
            return "DevTestUser";
        }

        private void _handleAdaptiveCardAction(AdaptiveCardLayout card, ActionEventArgs args)
        {
            Task.Factory.StartNew(async () =>
            {
                if (this._client != null)
                {
                    try
                    {
                        // {{"action": "PAY NOW","dialogName": "DebtDialog","cardName": "PaymentOptions"}}
                        var data = args.Data as JObject;
                        var action = data["action"].Value<string>();

                        if (action != null)
                        {
                            await this.SendMessage(action);
                        }
                        else
                        {
                            await this.SendMessage(args.Action.Title);
                        }

                    }
                    catch (Exception ex)
                    {
                    }
                }
            });
        }

    }
}
