using System;
using System.Collections.Generic;
using System.Text;
using Xam.Forms.Markdown;
using Xamarin.Forms;

namespace Microsoft.Botframework.Xamarin.Helpers
{
    public class SentMarkdownTheme : MarkdownTheme
    {
        public SentMarkdownTheme()
        {
            BackgroundColor = DefaultBackgroundColor;
            Paragraph.ForegroundColor = DefaultTextColor;
            Heading1.ForegroundColor = DefaultTextColor;
            Heading1.BorderColor = DefaultSeparatorColor;
            Heading2.ForegroundColor = DefaultTextColor;
            Heading2.BorderColor = DefaultSeparatorColor;
            Heading3.ForegroundColor = DefaultTextColor;
            Heading4.ForegroundColor = DefaultTextColor;
            Heading5.ForegroundColor = DefaultTextColor;
            Heading6.ForegroundColor = DefaultTextColor;
            Link.ForegroundColor = DefaultAccentColor;
            Code.ForegroundColor = DefaultTextColor;
            Code.BackgroundColor = DefaultCodeBackground;
            Quote.ForegroundColor = DefaultQuoteTextColor;
            Quote.BorderColor = DefaultQuoteBorderColor;
            Separator.BorderColor = DefaultSeparatorColor;
        }

        public static readonly Color DefaultBackgroundColor = Color.Transparent;

        public static readonly Color DefaultAccentColor = Color.White;

        public static readonly Color DefaultTextColor = Color.White;

        public static readonly Color DefaultCodeBackground = Color.White;

        public static readonly Color DefaultSeparatorColor = Color.White;

        public static readonly Color DefaultQuoteTextColor = Color.White;

        public static readonly Color DefaultQuoteBorderColor = Color.White;
    }
}
