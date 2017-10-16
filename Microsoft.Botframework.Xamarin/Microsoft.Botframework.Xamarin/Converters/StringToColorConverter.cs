using System;
using System.Globalization;
using Xamarin.Forms;

namespace Microsoft.Botframework.Xamarin.Converters
{
    public class StringToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color color = Color.Black;

            string id = (string)value;

            if (id != null)
            {
                color = Color.White;
            }
            else
            {
                color = Color.LightGray;
            }

            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
