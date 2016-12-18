using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.ViewManagement;
using Prism.Commands;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using Quizlet.Services;
using Quizlet.Views;

namespace Quizlet.ViewModels
{
    /// <summary>
    /// Page with tour menu, sign up, sign in buttons.
    /// </summary>
    public class TourPageViewModel : ViewModelBase
    {
        #region Services

        private INavigationService navigationService;
        private ISettingsService applicationService;

        #endregion

        #region Constructor

        /// <summary>
        /// Initialize new intance 
        /// </summary>
        public TourPageViewModel(INavigationService navigationService, ISettingsService applicationService)
        {
            this.navigationService = navigationService;
            this.applicationService = applicationService;

            this.LoginCommand = DelegateCommand.FromAsyncHandler(this.OnLoginCommand);
        }

        #endregion

        #region Overrided methods

        /// <summary>
        /// Invokes when the page is navigated.
        /// </summary>
        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {

            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                var statusBar = StatusBar.GetForCurrentView();
                statusBar.BackgroundColor = Color.FromArgb(100, 33, 109, 207);
                statusBar.BackgroundOpacity = 1;
                statusBar.ForegroundColor = Colors.White;
            }

            if (this.applicationService.GetToken() != null)
                this.navigationService.Navigate("Main",null);

            //base.OnNavigatedTo(e, viewModelState);

            
        }

        #endregion

        #region Commands

        /// <summary>
        /// Navigate to login page.
        /// </summary>
        public DelegateCommand LoginCommand { get; }

        #endregion

        #region Command handlers

        /// <summary>
        /// Invokes when LoginCommand is called.
        /// </summary>
        public Task OnLoginCommand()
        {
            this.navigationService.Navigate("Login",null);
            return Task.FromResult<object>(null);
        }

        #endregion

    }
}
