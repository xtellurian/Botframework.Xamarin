using CoreGraphics;
using Microsoft.Botframework.Xamarin.iOS.Effects;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("conversation.Effects")]
[assembly: ExportEffect(typeof(iOSKeyboardDoneButtonEffect), "KeyboardDoneButtonEffect")]

namespace Microsoft.Botframework.Xamarin.iOS.Effects
{
    public class iOSKeyboardDoneButtonEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            var textField = Control as UITextField;
            if (textField != null)
            {
                var toolbar = new UIToolbar(new CGRect(0.0f, 0.0f, Control.Frame.Size.Width, 44.0f));
                toolbar.Items = new[]
                {
                    new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace),
                    new UIBarButtonItem(UIBarButtonSystemItem.Done, delegate { Control.ResignFirstResponder(); })
                };
                textField.InputAccessoryView = toolbar;
            }
        }

        protected override void OnDetached()
        {

        }
    }
}
