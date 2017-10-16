using System;
using System.Linq;
using AdaptiveCards;
using AdaptiveCards.Rendering;
using Microsoft.Botframework.Xamarin.CustomViews;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(AdaptiveCardLayout), typeof(Microsoft.Botframework.Xamarin.iOS.Renderers.AdaptiveCardLayoutRendereriOS))]

namespace Microsoft.Botframework.Xamarin.iOS.Renderers
{
    public class AdaptiveCardLayoutRendereriOS : ViewRenderer<AdaptiveCardLayout, UIKit.UIView>
    {
        private AdaptiveCardRenderer _adaptiveCardsRenderer;

        public AdaptiveCardLayoutRendereriOS()
        {
            _adaptiveCardsRenderer = new AdaptiveCardRenderer();
        }

        protected override void OnElementChanged(ElementChangedEventArgs<AdaptiveCardLayout> e)
        {
            if (e.NewElement != null)
            {
                var activity = e.NewElement.BindingContext as Bot.Connector.DirectLine.Activity;

                if (activity?.Attachments != null &&
                    activity.Attachments.Any(m => m.ContentType == "application/vnd.microsoft.card.adaptive"))
                {
                    var cardAttachments = activity.Attachments.Where(m => m.ContentType == "application/vnd.microsoft.card.adaptive");

                    foreach (var attachment in cardAttachments)
                    {
                        var jObject = (JObject)attachment.Content;
                        AdaptiveCards.AdaptiveCard card;
                        try
                        {
                            card = jObject.ToObject<AdaptiveCard>();
                        }
                        catch (Exception ex)
                        {
                            return;
                        }

                        Device.BeginInvokeOnMainThread(() =>
                        {
                            if (!e.NewElement.Children.Any())
                            {
                                var xaml = _adaptiveCardsRenderer.RenderCard(card);

                                if (xaml.View != null)
                                {
                                    xaml.OnAction += (s, args) => e.NewElement.InvokeOnAction(s, args);

                                    xaml.View.WidthRequest = 350;
                                    xaml.View.Margin = new Thickness(8);
                                    xaml.View.BackgroundColor = Color.LightGray;

                                    e.NewElement.Children.Add(xaml.View);

                                    MessagingCenter.Send(this, "ReloadUITableViewData");

                                }
                                else
                                {
                                    e.NewElement.Children.Add(new Label() { Text = activity.Summary });
                                }
                            }
                        });
                    }
                }
            }
            base.OnElementChanged(e);
        }
    }
}