using System;
using Windows.UI.Xaml.Data;

namespace Nocturne.App.ValueConverters
{
    // Apparently can't display product price (decimal) in ProductViewPage without special converter. Crashes otherwise.
    public sealed class DecimalToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            decimal number;
            if (decimal.TryParse(value as string, out number)) return number;
            return decimal.Zero;
        }
    }
}