﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Quizlet.Converters
{
    class BoolToVisibleConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var bl = value as bool?;
            if (bl==true)
                return Visibility.Visible;
            else
                return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
