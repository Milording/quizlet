using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Prism.Commands;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using Quizlet.Models;
using Quizlet.Services;

namespace Quizlet.ViewModels
{
    /// <summary>
    /// Viewmodel for login page.
    /// </summary>
    public class LoginPageViewModel : ViewModelBase
    {
        #region Services

        private INavigationService pageNavigationService;
        private IApiService apiService;
        private ISettingsService settingsService;

        #endregion

        #region Commands

        public DelegateCommand<WebViewNavigationCompletedEventArgs> NavigationCompletedCommand { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates new instance of login page view model.
        /// </summary>
        public LoginPageViewModel(INavigationService pageNavigationService, IApiService apiService,
            ISettingsService settingsService)
        {
            this.pageNavigationService = pageNavigationService;
            this.apiService = apiService;
            this.settingsService = settingsService;

            this.NavigationCompletedCommand =
                new DelegateCommand<WebViewNavigationCompletedEventArgs>(OnNavigationCompleted);
        }

        #endregion

        #region Overrided methods

        /// <summary>
        /// Invokes when page is navigated
        /// </summary>
        public async void OnNavigationCompleted(WebViewNavigationCompletedEventArgs args)
        {
            if (!args.Uri.OriginalString.Contains("www.example.com"))
                return;
            var query = QueryToCollection(args.Uri.Query);
            var clientId = query.GetValues("code").First();

            var param = new Dictionary<string, string>()
            {
                {"grant_type", "authorization_code"},
                {"code", clientId},
                {"scope", "read"}
            };

            var token = await this.apiService.PostRequest(param, "oauth/token", null);
            this.settingsService.SetToken(token);

            this.pageNavigationService.Navigate("Main", null);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Convert plain text query to collection.
        /// </summary>
        private NameValueCollection QueryToCollection(string s)
        {
            NameValueCollection nvc = new NameValueCollection();
            // remove anything other than query string from url
            if (s.Contains("?"))
            {
                s = s.Substring(s.IndexOf('?') + 1);
            }
            foreach (string vp in Regex.Split(s, "&"))
            {
                var singlePair = Regex.Split(vp, "=");
                if (singlePair.Length == 2)
                {
                    nvc.Add(singlePair[0], singlePair[1]);
                }
                else
                {
                    // only one key with no value specified in query string
                    nvc.Add(singlePair[0], string.Empty);
                }
            }
            return nvc;
        }

        #endregion
    }
}
