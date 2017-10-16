using FreshMvvm;
using Microsoft.Botframework.Xamarin.Contract;
using Microsoft.Botframework.Xamarin.Implementations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Botframework.Xamarin
{
    public abstract class SharedGlue
    {
        public virtual IFreshIOC Register(IFreshIOC container)
        {
            container.Register<IBotService, BotService>();
            return container;
        }
    }
}
