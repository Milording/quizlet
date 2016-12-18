using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Collections;
using Windows.Media.SpeechSynthesis;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using Quizlet.Controls;
using Quizlet.Models;
using Quizlet.Services;

namespace Quizlet.ViewModels
{
    /// <summary>
    /// Main application page.
    /// </summary>
    public class MainPageViewModel : ViewModelBase
    {

        #region Services

        private INavigationService navigationService;
        private IApiService apiService;
        private ISettingsService settingsService;

        #endregion
        
        #region Constructor

        /// <summary>
        /// Initialize new intance of MainPageViewModel class.
        /// </summary>
        public MainPageViewModel(INavigationService navigationService, IApiService apiService, ISettingsService settingsService)
        {
            this.navigationService = navigationService;
            this.apiService = apiService;
            this.settingsService = settingsService;

            this.ListItemClickCommand= new DelegateCommand<ItemClickEventArgs>(this.OnListItemClickCommand);
            this.RefreshCommand = new DelegateCommand(this.OnRefreshCommand);
        }

        #endregion

        #region Overrided methods

        /// <summary>
        /// Invokes when page is navigated.
        /// </summary>
        public override async void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            RefreshLatestList();
            
            base.OnNavigatedTo(e, viewModelState);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Refreshes list of latest sets
        /// </summary>
        private async void RefreshLatestList()
        {
            this.IsLoading = true;

            var response =
                await
                    this.apiService.GetRequest(String.Format("users/{0}/studied",
                        this.settingsService.GetToken().user_id));

            this.IsLoading = false;

            if (!response.IsSuccessful)
            {
                var dialog = new MessageDialog(response.ErrorMessage, "Error");
                //await dialog.ShowAsync();

                if (this.SetList == null || this.SetList.Count == 0)
                {
                    this.IsLatestListUpdatedError = true;
                }
                return;
            }
            
            var sets = JsonConvert.DeserializeObject<List<UserStudy>>(response.Content);
            var groups = sets.GroupBy(s => s.FinishDate);
            var list = groups.Select(s => new GroupInfoList() {Key = s.Key, Items = s.ToList()});
            this.Directions = list.ToList();
            this.GroupedList = groups;
            this.SetList = new ObservableCollection<UserStudy>(sets);
        }


        #endregion

        #region Bindable properties

        private List<GroupInfoList> directions;

        public List<GroupInfoList> Directions
        {
            get { return this.directions; }
            set { SetProperty(ref this.directions, value); }
        }

        private IEnumerable<IGrouping<string, UserStudy>> groupedList;
        /// <summary>
        /// Gets or sets grouped list.
        /// </summary>
        public IEnumerable<IGrouping<string, UserStudy>> GroupedList
        {
            get { return this.groupedList; }
            set { SetProperty(ref this.groupedList, value); }
        }

        private bool isLatestListUpdatedError;
        /// <summary>
        /// Gets or sets status of getting list of sets.
        /// </summary>
        public bool IsLatestListUpdatedError
        {
            get { return this.isLatestListUpdatedError; }
            set { this.SetProperty(ref this.isLatestListUpdatedError, value); }
        }

        private bool isLoading;
        /// <summary>
        /// Defines whether the page is loading or not.
        /// </summary>
        public bool IsLoading
        {
            get { return this.isLoading; }
            set
            {
                Debug.WriteLine(value);
                this.SetProperty(ref this.isLoading, value);
            }
        }

        private ObservableCollection<UserStudy> setList;
        /// <summary>
        /// Gets or sets list of card sets.
        /// </summary>
        public ObservableCollection<UserStudy> SetList
        {
            get { return setList; }
            set
            {
                //setList = value;
                this.SetProperty(ref this.setList, value);
            }
        }

        #endregion

        #region Commands

        /// <summary>
        /// Invokes when pivot is changed selection.
        /// </summary>
        public DelegateCommand<ItemClickEventArgs> ListItemClickCommand{ get; private set; }

        /// <summary>
        /// Invokes when refresh button is clicked.
        /// </summary>
        public DelegateCommand RefreshCommand { get; private set; }

        #endregion

        #region Command handlers    

        /// <summary>
        /// Invokes when pivot header is changed.
        /// </summary>
        public void OnPivotSelectionChangedCommand(SelectionChangedEventArgs e)
        {
            if (e.RemovedItems.Count > 0)
                VisualStateManager.GoToState((e.RemovedItems.First() as PivotItem).Header as TabHeader, "Unselected",
                    true);
            if (e.AddedItems.Count > 0)
                VisualStateManager.GoToState((e.AddedItems.First() as PivotItem).Header as TabHeader, "Selected", true);
        }

        /// <summary>
        /// Invokes when set list is changed selection.
        /// </summary>
        public void OnListItemClickCommand(ItemClickEventArgs e)
        {
            var userStudy = e.ClickedItem as UserStudy;
            if (userStudy != null)
            {
                this.navigationService.Navigate("SetDetail", userStudy.set);
            }
            else
            {
                Debug.WriteLine("Wrong selected item.");
            }
        }

        /// <summary>
        /// Invokes when update button is pressed.
        /// </summary>
        public async void OnRefreshCommand()
        {
            RefreshLatestList();
        }

        #endregion

    }
}