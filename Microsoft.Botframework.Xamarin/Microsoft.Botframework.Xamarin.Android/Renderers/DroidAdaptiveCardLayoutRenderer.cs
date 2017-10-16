using System;
using System.Linq;
using AdaptiveCards;
using AdaptiveCards.Rendering;
using Microsoft.Botframework.Xamarin.CustomViews;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Microsoft.Botframework.Xamarin.CustomViews.AdaptiveCardLayout), typeof(Microsoft.Botframework.Xamarin.Droid.Renderers.DroidAdaptiveCardLayoutRenderer))]

namespace Microsoft.Botframework.Xamarin.Droid.Renderers
{
    public class DroidAdaptiveCardLayoutRenderer : ViewRenderer<AdaptiveCardLayout, Android.Views.View>
    {
        // private AdaptiveCardRenderer _adaptiveCardsRenderer;

        public DroidAdaptiveCardLayoutRenderer()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<AdaptiveCardLayout> e)
        {
            var renderer = new AdaptiveCardRenderer();
            var activity = e.NewElement.BindingContext as Bot.Connector.DirectLine.Activity;
            var oldActivity = e.OldElement?.BindingContext as Bot.Connector.DirectLine.Activity;
            if (!string.Equals(oldActivity?.Id, activity?.Id))
            {
                if (activity.Attachments != null &&
                activity.Attachments.Any(m => m.ContentType == "application/vnd.microsoft.card.adaptive"))
                {
                    var cardAttachments = activity.Attachments.Where(m => m.ContentType == "application/vnd.microsoft.card.adaptive");

                    foreach (var attachment in cardAttachments)
                    {
                        var jObject = (JObject)attachment.Content;
                        AdaptiveCard card = null;
                        try
                        {
                            card = jObject.ToObject<AdaptiveCard>();
                        }
                        catch (Exception ex)
                        {
                            // GETTING Deserializing error here
                            // 		Message	"Error converting value \"number\" to type 'AdaptiveCards.TextInputStyle'. Path 'style'."	string


                        }

                        Device.BeginInvokeOnMainThread(() =>
                        {
                            if (!e.NewElement.Children.Any())
                            {
                                var xaml = renderer.RenderCard(card);
                                // RecurseForButtons(xaml.View);

                                if (xaml.View != null)
                                {
                                    xaml.OnAction += (s, args) => e.NewElement.InvokeOnAction(s, args);

                                    xaml.View.WidthRequest = 350;
                                    xaml.View.Margin = new Thickness(8);
                                    xaml.View.BackgroundColor = Color.LightGray;

                                    e.NewElement.Children.Add(xaml.View);

                                    MessagingCenter.Send(this, "ScrollToBottom");
                                }
                                else
                                {
                                    e.NewElement.Children.Add(new Label() { Text = activity.Summary });
                                }
                            }
                        });
                    }
                }
                base.OnElementChanged(e);
            }
        }
    }
}