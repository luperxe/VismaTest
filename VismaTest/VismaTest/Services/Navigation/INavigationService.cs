using VismaTest.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace VismaTest.Services.Navigation
{
    public interface INavigationService
    {
        Task InitializeAsync();


        #region Navegaciones MAINPAGE
        Task NavigateToAsync<TViewModel>(object parameter = null) where TViewModel : BaseViewModel;

        Task PopupToAsync<TViewModel>(object parameter = null) where TViewModel : BaseViewModel;

        Task NavigateModalToAsync<TViewModel>(object parameter = null) where TViewModel : BaseViewModel;

        Task NavigateToModalWithNavigationAsync(Type viewModelType, object parameter = null);

        Task<Page> NavigateBackAsync();

        Task<Page> NavigateBackModalAsync();

        Task<Page> NavigateCloseModalAsync();
        #endregion

        #region Utils
        Task<bool> RemovePreviousPage();

        Task<bool> RemoveToHomePage();
        #endregion
    }
}
