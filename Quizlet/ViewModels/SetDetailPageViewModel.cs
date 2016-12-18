using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using Quizlet.Models;
using Quizlet.Services;

namespace Quizlet.ViewModels
{
    /// <summary>
    /// Page which contains full information about user set.
    /// </summary>
    public class SetDetailPageViewModel : ViewModelBase
    {
        #region Services

        private INavigationService navigationService;
        private IApiService apiService;
        private ISettingsService applicationService;

        #endregion

        #region Constructor

        /// <summary>
        /// Initialize a new instance of SetDetailPage
        /// </summary>
        public SetDetailPageViewModel(INavigationService navigationService, IApiService apiService,
            ISettingsService applicationService)
        {
            this.navigationService = navigationService;
            this.apiService = apiService;
            this.applicationService = applicationService;

            this.ListItemClickCommand = new DelegateCommand(this.OnListItemClockCommand);
        }

        #endregion

        #region Overrided events

        /// <summary>
        /// Invokes when page is navigated.
        /// </summary>
        public override async void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            this.IsLoading = true;

            this.ContextSet = e.Parameter as Set;

            var currentView = SystemNavigationManager.GetForCurrentView();
            //currentView.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            //currentView.BackRequested += ((s, d) => { this.navigationService.GoBack(); });

            var context = this.ContextSet;
            if (context != null)
            {
                var response = await this.apiService.GetRequest(String.Format("sets/{0}", context.id));
                var set = JsonConvert.DeserializeObject<Set>(response.Content);
                this.ListOfTerms = set.terms;
            }

            this.IsLoading = false;

            base.OnNavigatedTo(e, viewModelState);
        }

        #endregion

        #region Commands

        /// <summary>
        /// Invokes when list item is clicked.
        /// </summary>
        public DelegateCommand ListItemClickCommand { get; }

        #endregion 

        #region Command handlers

        /// <summary>
        /// Navigate page to page with all cards of set.
        /// </summary>
        public void OnListItemClockCommand()
        {  
            this.navigationService.Navigate("AllCards", this.listOfTerms);
        }

        #endregion

        #region Bindable properties

        private bool isLoading;
        /// <summary>
        /// Defines whether the page is loading or not.
        /// </summary>
        public bool IsLoading
        {
            get { return this.isLoading; }
            set { this.SetProperty(ref this.isLoading, value); }
        }

        private Set contextSet;
        /// <summary>
        /// Context of selected user set.
        /// </summary>
        public Set ContextSet
        {
            get { return contextSet; }
            set { this.SetProperty(ref this.contextSet, value); }
        }

        private ObservableCollection<Term> listOfTerms;

        /// <summary>
        /// List of terms.
        /// </summary>
        public ObservableCollection<Term> ListOfTerms
        {
            get { return listOfTerms; }
            set { this.SetProperty(ref this.listOfTerms, value); }
        }

        #endregion
    }
}
