using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Quizlet.Services;
using Quizlet.Tools.Navigation;

namespace Quizlet.ViewModels
{
    public class ViewModelBase : Screen
    {
        protected readonly IPageNavigationService PageNavigationService;
        protected readonly IApiService ApiService;
        protected readonly IApplicationService ApplicationService;


        protected ViewModelBase(IPageNavigationService pageNavigationService,IApiService apiService, IApplicationService applicationService)
        {
            this.PageNavigationService = pageNavigationService;
            this.ApiService = apiService;
            this.ApplicationService = applicationService;
        }

        protected void NavigateTo<T>()
        {
            PageNavigationService.NavigateTo<T>();
        }
    }
}
