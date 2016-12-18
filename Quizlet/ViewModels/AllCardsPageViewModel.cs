using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.SpeechSynthesis;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Prism.Commands;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using Quizlet.Models;
using Quizlet.Services;
using Windows.UI.ViewManagement;

namespace Quizlet.ViewModels
{
    /// <summary>
    /// Page with all cards with flipping and listening.
    /// </summary>
    public class AllCardsPageViewModel : ViewModelBase
    {

        #region Services

        private INavigationService navigationService;
        private ISettingsService applicationService;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of <see cref="AllCardsPageViewModel"/>.
        /// </summary>
        public AllCardsPageViewModel(INavigationService navigationService, ISettingsService applicationService)
        {
            this.navigationService = navigationService;
            this.applicationService = applicationService;

            this.MusicTappedCommand = new DelegateCommand<TappedRoutedEventArgs>(this.OnMusicTappedCommand);
            this.HeightOfDevice = (int)ApplicationView.GetForCurrentView().VisibleBounds.Height;
        }

        #endregion

        #region Commands

        /// <summary>
        /// Command invokes when user tapped on the speak icon.
        /// </summary>
        public DelegateCommand<TappedRoutedEventArgs> MusicTappedCommand { get; private set; }

        #endregion

        #region Bindable properties


        private ObservableCollection<TermCardViewModel> contextTerms;
        /// <summary>
        /// Context terms.
        /// </summary>
        public ObservableCollection<TermCardViewModel> ContextTerms
        {
            get { return this.contextTerms; }
            set { this.SetProperty(ref this.contextTerms, value); }
        }

        private int heightOfDevice;
        /// <summary>
        /// Screen width of the device.
        /// </summary>
        public int HeightOfDevice
        {
            get { return this.heightOfDevice; }
            set { this.SetProperty(ref this.heightOfDevice, value); }
        }


        #endregion

        #region Overrided events

        /// <summary>
        /// Invokes when page is navigated.
        /// </summary>
        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            var terms = e.Parameter as ObservableCollection<Term>;
            var termVM = new ObservableCollection<TermCardViewModel>();
            
            foreach (var term in terms)
            {
                termVM.Add(new TermCardViewModel(this.Speak,term));
            }

            this.ContextTerms = termVM;
            
            base.OnNavigatedTo(e, viewModelState);
        }

        #endregion

        #region Command handlers

        /// <summary>
        /// Invokes when user tapped on the speak icon.
        /// </summary>
        public async void OnMusicTappedCommand(TappedRoutedEventArgs e)
        {
            Debug.WriteLine("Hey!");
        }

        public void TappedDefinition(object sender)
        {
            var term = sender as Term;
            if (term != null) Speak(term.definition);
        }


        #endregion

        #region Private methods

        /// <summary>
        /// Speaks text in the argument.
        /// </summary>
        async Task Speak(string text)
        {
            // The media object for controlling and playing audio.
            MediaElement mediaElement = new MediaElement();

            // The object for controlling the speech synthesis engine (voice).
            var synth = new SpeechSynthesizer();
            var allVoices = SpeechSynthesizer.AllVoices;
            synth.Voice = SpeechSynthesizer.AllVoices.First();
            // Generate the audio stream from plain text.
            SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync(text);
            

            // Send the stream to the media object.
            //mediaElement.Language = "ru-RU";
            mediaElement.SetSource(stream, stream.ContentType);
            //mediaElement.Play();

        }

        #endregion

    }
}
