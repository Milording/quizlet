using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Newtonsoft.Json;
using Quizlet.Enums;
using Quizlet.Models;

namespace Quizlet.Services
{
    /// <summary>
    /// Service for local storing settings of application.
    /// </summary>
    public class SettingsService : ISettingsService
    {
        #region Private fields

        readonly ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

        #endregion

        #region Contructor

        /// <summary>
        /// Creates new intances of SettingService.
        /// </summary>
        public SettingsService()
        {
        }

        #endregion

        #region Public methods
        
        /// <summary>
        /// Checks if local application data contains a key.
        /// </summary>
        public bool Contains(string key)
        {
            return this.localSettings.Values.ContainsKey(key);
        }

        /// <summary>
        /// Add an object to local application data.
        /// </summary>
        public void Add(string key, string value)
        {
            this.localSettings.Values.Add(key, value);
        }

        /// <summary>
        /// Set an API token for working.
        /// </summary>
        public void SetToken(AuthToken token)
        {
            this.localSettings.Values[Settings.AuthToken] = JsonConvert.SerializeObject(token);
        }

        /// <summary>
        /// Gets an API token.
        /// </summary>
        public AuthToken GetToken()
        {
            var token =  this.localSettings.Values[Settings.AuthToken];
            if (token != null)
                return JsonConvert.DeserializeObject<AuthToken>(token as string);
            return null;
        }

        #endregion
    }
}
