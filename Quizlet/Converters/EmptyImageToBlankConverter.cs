using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Quizlet.Converters
{
    class EmptyImageToBlankConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var s = value as string;
            if(s != null && s.Contains("hprofile-ak-xfp1"))
                return "/Assets/blankAvatar.png";
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw null;
        }
    }
}
