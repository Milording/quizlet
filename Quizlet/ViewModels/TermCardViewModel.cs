using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Prism.Commands;
using Prism.Windows.Mvvm;
using Quizlet.Models;

namespace Quizlet.ViewModels
{
    /// <summary>
    /// The view model for term card.
    /// </summary>
    public class TermCardViewModel:ViewModelBase
    {

        #region Observable properties

        private Term term;
        /// <summary>
        /// Gets or sets term to card.
        /// </summary>
        public Term Term
        {
            get { return this.term; }
            set { this.SetProperty(ref this.term, value); }
        }

        private string termLanguage;
        /// <summary>
        /// Language of term.
        /// </summary>
        public string TermLanguage
        {
            get { return this.termLanguage; }
            set { this.SetProperty(ref this.termLanguage, value); }
        }

        private string definitionLanguage;
        /// <summary>
        /// Language of definition.
        /// </summary>
        public string DefinitionLanguage
        {
            get { return this.definitionLanguage; }
            set { this.SetProperty(ref this.definitionLanguage, value); }
        }

        #endregion

        #region Commands 

        /// <summary>
        /// Gets the command to speak the term.
        /// </summary>
        public DelegateCommand<string> SpeakCommand { get; private set; }

        #endregion

        #region Constructors 

        /// <summary>
        /// Initialize a new instance of <see cref="TermCardViewModel"/>>
        /// </summary>
        public TermCardViewModel()
        {
        }

        /// <summary>
        /// Initialize a new instance of <see cref="TermCardViewModel"/>>
        /// </summary>
        public TermCardViewModel(Func<string, Task> speakCommandHandler, Term term)
        {
            this.SpeakCommand = DelegateCommand<string>.FromAsyncHandler(speakCommandHandler);
            this.Term = term;
        }

        #endregion

    }
}
