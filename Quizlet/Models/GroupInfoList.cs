using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizlet.Models
{
    /// <summary>
    /// Class which contains key for grouping list.
    /// </summary>
    public class GroupInfoList : List<object>
    {
        /// <summary>
        /// Gets or sets key of group.
        /// </summary>
        public object Key { get; set; }

        public List<UserStudy> Items { get; set; }
    }
}
