using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizlet.Models
{   
    /// <summary>
    /// Class for containing information about response.
    /// </summary>
    public class QuizletResponse
    {
        /// <summary>
        /// Gets or sets successful status.
        /// </summary>
        public bool IsSuccessful { get; set; }

        /// <summary>
        /// Gets or sets error message.
        /// </summary>

        public string ErrorMessage { get; set; }
        /// <summary>
        /// Gets or sets content of response. 
        /// </summary>
        public string Content { get; set; }
    }
}
