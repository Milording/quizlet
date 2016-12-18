using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quizlet.Converters;

namespace Quizlet.Models
{
    public class Image
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Creator
    {
        public string username { get; set; }
        public string account_type { get; set; }
        public string profile_image { get; set; }
        public int id { get; set; }
    }

    public class Term
    {
        public object id { get; set; }
        public string term { get; set; }
        public string definition { get; set; }
        public Image image { get; set; }
        public int rank { get; set; }
    }

    public class Set
    {
        public int id { get; set; }
        public string url { get; set; }
        public string title { get; set; }
        public string created_by { get; set; }
        public int term_count { get; set; }
        public int created_date { get; set; }
        public int modified_date { get; set; }
        public int published_date { get; set; }
        public bool has_images { get; set; }
        public List<object> subjects { get; set; }
        public string visibility { get; set; }
        public string editable { get; set; }
        public bool has_access { get; set; }
        public bool can_edit { get; set; }
        public string description { get; set; }
        public string lang_terms { get; set; }
        public string lang_definitions { get; set; }
        public int password_use { get; set; }
        public int password_edit { get; set; }
        public int access_type { get; set; }
        public int creator_id { get; set; }
        public Creator creator { get; set; }
        public List<object> class_ids { get; set; }
        public ObservableCollection<Term> terms { get; set; }
        public object display_timestamp { get; set; }
    }

    public class UserStudy
    {
        public string mode { get; set; }
        public object id { get; set; }

        public string StartDate
        {
            get
            {
                var converter = new UnixTimeToDateTimeConverter();
                return (string) converter.Convert(this.start_date, typeof (DateTime), null,null);
            }
        }

        public string FinishDate { get
            {
                var converter = new UnixTimeToDateTimeConverter();
                return (string)converter.Convert(this.finish_date, typeof(DateTime), null, null);
            }
        }

        public int start_date { get; set; }

        public int? finish_date { get; set; }
        public string formatted_score { get; set; }
        public Set set { get; set; }
        public string image_url { get; set; }
    }
}
