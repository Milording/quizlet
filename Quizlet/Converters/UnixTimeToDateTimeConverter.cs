using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Quizlet.Converters
{
    /// <summary>
    /// Converts UNIX time to DateTime.
    /// </summary>
    class UnixTimeToDateTimeConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            string formattedDateTime = null;
            if (value != null)
            {
                formattedDateTime = dtDateTime.AddSeconds(System.Convert.ToDouble(value)).ToLocalTime().ToString("yyyy MMMM dd");
            }
            return formattedDateTime;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
