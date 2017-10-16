using AdaptiveCards.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Microsoft.Botframework.Xamarin.CustomViews
{
    public class AdaptiveCardLayout : StackLayout
    {
        public event EventHandler OnAction;

        public void InvokeOnAction(object sender, ActionEventArgs args)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                MessagingCenter.Send(this, "AdaptiveCardAction", args);
            });
        }
    }
}
