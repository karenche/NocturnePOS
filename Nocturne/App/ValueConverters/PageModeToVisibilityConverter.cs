using Nocturne.App.Helpers;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Nocturne.App.ValueConverters
{
    public sealed class PageModeToVisibilityConverter : IValueConverter
    {
        public PageMode CheckedValue { get; set; }

        public Visibility TrueValue { get; set; }

        public Visibility FalseValue { get; set; }

        public PageModeToVisibilityConverter()
        {
            CheckedValue = PageMode.View;
            TrueValue = Visibility.Visible;
            FalseValue = Visibility.Collapsed;
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (!(value is PageMode))
                return null;
            return (PageMode)value == CheckedValue ? TrueValue : FalseValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }
    }
}
