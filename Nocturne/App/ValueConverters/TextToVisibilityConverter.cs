using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Nocturne.App.ValueConverters
{
    public sealed class TextToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var strValue = value as string;
            return string.IsNullOrEmpty(strValue) ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }
    }
}
