using Nocturne.App.Helpers;
using System;
using System.Globalization;
using Windows.UI.Xaml.Data;

namespace Nocturne.App.ValueConverters
{
    public sealed class PageModeToBooleanConverter : IValueConverter
    {
        public PageMode CheckedValue { get; set; }

        public bool TrueValue { get; set; } 

        public PageModeToBooleanConverter()
        {
            CheckedValue = PageMode.View;
            TrueValue = true;
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (!(value is PageMode))
                return null;
            return (PageMode)value == CheckedValue ? TrueValue : !TrueValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }
    }
}
