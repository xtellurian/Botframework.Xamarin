using Microsoft.Bot.Connector.DirectLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Botframework.Xamarin.Model
{
    public class SendPhotoActivity : Activity
    {
        public SendPhotoActivity()
        {
        }

        public string Path { get; set; }
    }
}
