using Nocturne.App.Helpers;
using Nocturne.BL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Data;

namespace Nocturne.App.ValueConverters
{
    public sealed class ValidationMessageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {                           
            var collection = value as Dictionary<string, IValidationMessage[]>;
            var collectionKey = parameter as string;
            if (collection == null || collectionKey == null) { return string.Empty; }  
            if (!collection.ContainsKey(collectionKey)) { return string.Empty; }
            return collection[collectionKey].FirstOrDefault().Message;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }
    }
}
