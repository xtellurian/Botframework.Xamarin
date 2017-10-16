using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Foundation;
using Microsoft.Bot.Connector.DirectLine;
using Microsoft.Botframework.Xamarin.CustomViews;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ChatListView), typeof(Microsoft.Botframework.Xamarin.iOS.Renderers.ChatListViewRendereriOS))]

namespace Microsoft.Botframework.Xamarin.iOS.Renderers
{
    public class ChatListViewRendereriOS : ListViewRenderer
    {
        private ObservableCollection<Activity> _observableCollection;

        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                if (_observableCollection != null)
                {
                    _observableCollection.CollectionChanged -= CollectionChanged;
                }

                MessagingCenter.Unsubscribe<AdaptiveCardLayoutRendereriOS>(this, "ReloadUITableViewData");
            }

            if (e.NewElement != null)
            {
                var chatListView = e.NewElement as ChatListView;
                if (chatListView != null)
                {
                    _observableCollection = chatListView.ItemsSource as ObservableCollection<Activity>;
                    if (_observableCollection != null)
                    {
                        _observableCollection.CollectionChanged += CollectionChanged; ;
                    }
                }

                MessagingCenter.Subscribe<AdaptiveCardLayoutRendereriOS>(this, "ReloadUITableViewData", (sender) => {

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Control.ReloadData();
                        ScrollToLastRow();
                    });
                });
            }
        }

        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                ScrollToLastRow();
            });
        }

        private void ScrollToLastRow()
        {
            var section = Control.NumberOfSections() - 1;
            var row = Control.NumberOfRowsInSection(section) - 1;
            var indexPath = NSIndexPath.FromRowSection(row, section);

            Control.ScrollToRow(indexPath, UIKit.UITableViewScrollPosition.Bottom, false);
        }
    }
}