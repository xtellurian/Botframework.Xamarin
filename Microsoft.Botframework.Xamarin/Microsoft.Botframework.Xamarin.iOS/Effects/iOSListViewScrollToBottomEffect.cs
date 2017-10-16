using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

using Foundation;
using Microsoft.Bot.Connector.DirectLine;
using Microsoft.Botframework.Xamarin.iOS.Effects;
using Microsoft.Botframework.Xamarin.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportEffect(typeof(iOSListViewScrollToBottomEffect), "ListViewScrollToBottomEffect")]

namespace Microsoft.Botframework.Xamarin.iOS.Effects
{
    public class iOSListViewScrollToBottomEffect : PlatformEffect
    {
        private ObservableCollection<Activity> _observableCollection;
        private UITableView _tableView;

        protected override void OnAttached()
        {
            var _tableView = Control as UITableView;
            if (_tableView != null)
            {
                var listView = Element as ListView;
                if (listView != null)
                {
                    _observableCollection = listView.ItemsSource as ObservableCollection<Activity>;
                    if (_observableCollection != null)
                    {
                        _observableCollection.CollectionChanged += CollectionChanged;

                    }
                }

                MessagingCenter.Subscribe<AdaptiveCardLayoutRendereriOS>(this, "ReloadUITableViewData", (sender) => {
                    if (_tableView != null)
                    {
                        _tableView.ReloadData();
                    }
                    ScrollToLastRow();
                });
            }
        }

        protected override void OnDetached()
        {
            if (_observableCollection != null)
            {
                _observableCollection.CollectionChanged -= CollectionChanged;
            }

            MessagingCenter.Unsubscribe<AdaptiveCardLayoutRendereriOS>(this, "ReloadUITableViewData");
        }

        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            ScrollToLastRow();
        }

        private void ScrollToLastRow()
        {
            if (_tableView != null)
            {
                var section = _tableView.NumberOfSections() - 1;
                var row = _tableView.NumberOfRowsInSection(section) - 1;
                var indexPath = NSIndexPath.FromRowSection(row, section);

                _tableView.ScrollToRow(indexPath, UITableViewScrollPosition.Bottom, false);
            }
        }
    }
}