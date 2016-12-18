using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.Practices.Unity;
using Newtonsoft.Json;
using Prism.Unity.Windows;
using Prism.Windows.Navigation;
using Quizlet.Messages;
using Quizlet.Services;
using Quizlet.ViewModels;
using Quizlet.Views;

namespace Quizlet
{
    /// <summary>
    /// Main class for definition of application.
    /// </summary>
    sealed partial class App : PrismUnityApplication
    {
        private SettingsService settingsService;

        /// <summary>
        /// Initialize the intance of application.
        /// </summary>
        public App()
        {
            this.InitializeComponent();
        }

        #region Overrided methods

        /// <summary>
        /// Invokes when application is launching.
        /// </summary>
        protected override Task OnInitializeAsync(IActivatedEventArgs args)
        {
            Container.RegisterInstance(this.NavigationService);
            
            Container.RegisterType<IApiService, ApiService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ISettingsService, SettingsService>(new ContainerControlledLifetimeManager());
            
            this.settingsService=new SettingsService();

            return base.OnInitializeAsync(args);
        }
        

        protected override Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
        {

            if (this.settingsService.GetToken() != null)
            {
                NavigationService.Navigate("Main", null);
                return Task.FromResult<object>(null);
            }

            NavigationService.Navigate("Tour", null);
            return Task.FromResult<object>(null);
        }

        #endregion

    }
}
