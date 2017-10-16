using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using FreshMvvm;
using UIKit;

namespace Microsoft.Botframework.Xamarin.iOS
{
    public class iOSGlue : SharedGlue
    {
        public override IFreshIOC Register(IFreshIOC container)
        {
            return base.Register(container);
        }
    }
}