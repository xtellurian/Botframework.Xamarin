using System;
using System.Globalization;
using Xamarin.Forms;

namespace Microsoft.Botframework.Xamarin.Converters
{
    public class IdToSendStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = string.Empty;

            string id = (string)value;

            if (id != null)
            {
                result = "Sent";
            }
            else
            {
                result = "Sending";
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
