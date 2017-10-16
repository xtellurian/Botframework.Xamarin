using Microsoft.Botframework.Xamarin.Droid.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportEffect(typeof(DroidListViewStackFromBottomEffect), "ListViewStackFromBottomEffect")]

namespace Microsoft.Botframework.Xamarin.Droid.Effects
{
    public class DroidListViewStackFromBottomEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            var listView = Control as Android.Widget.ListView;
            if (listView != null)
            {
                listView.StackFromBottom = true;
            }
        }

        protected override void OnDetached()
        {
        }
    }
}