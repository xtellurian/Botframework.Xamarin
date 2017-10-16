using Microsoft.Bot.Connector.DirectLine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Botframework.Xamarin.Contract
{
    public interface IBotService
    {
        Task<string> StartConversation(bool isDefault = true);
        Task<ResourceResponse> SendMessage(string message, string conversationId = null);

        event EventHandler<ActivityEventArgs> ActivitySent;
        event EventHandler<ActivityEventArgs> ActivityReceived;
    }

    public class ActivityEventArgs : EventArgs
    {
        public Activity Activity;
    }
}
