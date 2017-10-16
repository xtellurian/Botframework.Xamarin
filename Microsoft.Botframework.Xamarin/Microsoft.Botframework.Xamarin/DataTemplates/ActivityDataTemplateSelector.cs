using Microsoft.Bot.Connector.DirectLine;
using Microsoft.Botframework.Xamarin.Model;
using System.Linq;
using Xamarin.Forms;

namespace Microsoft.Botframework.Xamarin.DataTemplates
{
    public class ActivityDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate SenderTemplate { get; set; }
        public DataTemplate ReceiverTemplate { get; set; }
        public DataTemplate AdaptiveCardsTemplate { get; set; }
        public DataTemplate SentPhotoAttachmentTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var activity = (Activity)item;
            if (activity.Attachments != null &&
                activity.Attachments.Any(m => m.ContentType == "application/vnd.microsoft.card.adaptive"))
            {
                return AdaptiveCardsTemplate;
            }
            if (activity is SendPhotoActivity)
            {
                if (((SendPhotoActivity)activity).Path != null && ((SendPhotoActivity)activity).Path != string.Empty)
                {
                    return SentPhotoAttachmentTemplate;
                }
            }

            return ((Activity)item).From.Id == "DevTestUser" ? SenderTemplate : ReceiverTemplate;
        }
    }
}
