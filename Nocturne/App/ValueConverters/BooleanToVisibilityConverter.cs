using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Nocturne.App.ValueConverters
{
    public sealed class BooleanToVisibilityConverter : IValueConverter
    {
        public Visibility TrueValue { get; set; }

        public Visibility FalseValue { get; set; }

        public BooleanToVisibilityConverter()
        {                                 
            TrueValue = Visibility.Visible;
            FalseValue = Visibility.Collapsed;
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (!(value is bool))
                return null;
            return (bool)value ? TrueValue : FalseValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }
    }
}
