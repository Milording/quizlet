using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quizlet.Enums;
using Quizlet.Models;

namespace Quizlet.Services
{
    /// <summary>
    /// Interface for settings service.
    /// </summary>
    public interface ISettingsService
    {
        /// <summary>
        /// Checks if local application data contains a key.
        /// </summary>
        bool Contains(string key);

        /// <summary>
        /// Add an object to local application data.
        /// </summary>
        void Add(string key, string value);

        /// <summary>
        /// Set an API token for working.
        /// </summary>
        void SetToken(AuthToken token);

        /// <summary>
        /// Gets an API token.
        /// </summary>
        AuthToken GetToken();
    }
}
