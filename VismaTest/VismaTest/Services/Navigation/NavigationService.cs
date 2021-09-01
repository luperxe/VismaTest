using VismaTest.Pages.GoogleAuth;
using VismaTest.Pages.Home;
using VismaTest.Pages.Weather;
using VismaTest.Services.WebServices;
using VismaTest.ViewModels.Base;
using VismaTest.ViewModels.GoogleAuth;
using VismaTest.ViewModels.Home;
using VismaTest.ViewModels.Weather;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace VismaTest.Services.Navigation
{
    public class NavigationService : INavigationService
    {
        
        #region vars y propeties
        protected readonly Dictionary<Type, Type> mappings;
        protected Application CurrentApplication
        {
            get

            {
                return Application.Current;
            }

        }
        #endregion

        #region Constructor
        public NavigationService()
        {
            mappings = new Dictionary<Type, Type>();
            CreatePageViewModelMappings();

        }

        #endregion

        #region Registrar los ViewModels y las vistas aqui
        private void CreatePageViewModelMappings()
        {

            mappings.Add(typeof(HomeViewModel), typeof(HomePage));
            mappings.Add(typeof(WeatherViewModel), typeof(WeatherPage));
            mappings.Add(typeof(CurrentLocationWeatherViewModel), typeof(CurrentLocationWeatherPage));
            mappings.Add(typeof(GoogleAuthViewModel), typeof(GoogleAuthPage));
        }

        #endregion


        public Task InitializeAsync()
        {
            Page page = CreateAndBindPage(typeof(HomeViewModel), null);

            CurrentApplication.MainPage = new NavigationPage(page);

           return Task.FromResult(false);
        }

        #region Navegaciones MAINPAGE
        public Task NavigateToAsync<TViewModel>(object parameter = null) where TViewModel : BaseViewModel
        {
            return NavigateToAsync(typeof(TViewModel), parameter);
        }

        public Task NavigateModalToAsync<TViewModel>(object parameter = null) where TViewModel : BaseViewModel
        {
            return NavigateToModalAsync(typeof(TViewModel), parameter);
        }

        public Task PopupToAsync<TViewModel>(object parameter = null) where TViewModel : BaseViewModel
        {
            return PopupAsync(typeof(TViewModel), parameter);
        }


        public virtual async Task NavigateToModalWithNavigationAsync(Type viewModelType, object parameter = null)
        {
            Page page = CreateAndBindPage(viewModelType, parameter);

            if (CurrentApplication.MainPage != null && CurrentApplication.MainPage.Navigation != null)
            {
                try
                {
                    Page navigation = new NavigationPage(page);
                    ((BaseViewModel)page.BindingContext).Navigation = navigation.Navigation; //Le pasamos el objeto de navegación
                    await CurrentApplication.MainPage.Navigation.PushModalAsync(navigation);
                }
                catch (Exception ex)
                {
                }
            }
        }


        public async Task<Page> NavigateBackAsync()
        {
            if (CurrentApplication.MainPage != null)
            {
                return await CurrentApplication.MainPage.Navigation.PopAsync();
            }
            return null;
        }

        public async Task<Page> NavigateBackModalAsync()
        {
            if (CurrentApplication.MainPage != null)
            {
                return await CurrentApplication.MainPage.Navigation.PopModalAsync();
            }
            return null;
        }


        public async Task<Page> NavigateCloseModalAsync()
        {
            return await NavigateBackModalAsync();
        }





        #endregion




        protected virtual async Task NavigateToAsync(Type viewModelType, object parameter)
        {
            Page page = CreateAndBindPage(viewModelType, parameter);

            if (page is HomePage)
            {
                CurrentApplication.MainPage = new NavigationPage(page);
            }
            else if (CurrentApplication.MainPage != null && CurrentApplication.MainPage.Navigation != null)
            {
                try
                {
                    await CurrentApplication.MainPage.Navigation.PushAsync(page);
                }
                catch (Exception ex)
                {
                    throw new Exception($"============ Navegation PushAsync error: " + ex.Message + "============");
                }
            }
        }

        protected virtual async Task NavigateToModalAsync(Type viewModelType, object parameter)
        {
            Page page = CreateAndBindPage(viewModelType, parameter);

            if (CurrentApplication.MainPage != null && CurrentApplication.MainPage.Navigation != null)
            {
                try
                {
                    await CurrentApplication.MainPage.Navigation.PushModalAsync(page);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        protected virtual async Task PopupAsync(Type viewModelType, object parameter)
        {
            PopupPage page = CreateAndBindPopupPage(viewModelType, parameter);

            if (CurrentApplication.MainPage != null && CurrentApplication.MainPage.Navigation != null)
            {
                try
                {
                    await PopupNavigation.PushAsync((Rg.Plugins.Popup.Pages.PopupPage)page);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public IReadOnlyList<Page> NavigationStack()
        {

            return CurrentApplication.MainPage.Navigation.NavigationStack;
        }

        public bool IsNamePrevioPage(Type viewModelType)
        {
            Page namePage = CurrentApplication.MainPage.Navigation.NavigationStack[CurrentApplication.MainPage.Navigation.NavigationStack.Count - 2];

            var vm = namePage.ToString().Replace("Page", "ViewModel");

            if (viewModelType.ToString() == vm)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        protected Page CreateAndBindPage(Type viewModelType, object parameter)

        {

            Type pageType = GetPageTypeForViewModel(viewModelType);



            if (pageType == null)

            {

                throw new Exception($"Mapping type for {viewModelType} is not a page");

            }



            Page page = Activator.CreateInstance(pageType) as Page;

            BaseViewModel viewModel = ViewModelLocator.Instance.Resolve(viewModelType) as BaseViewModel;



            viewModel.FormsViewDidLoadAsync(parameter);

            page.BindingContext = viewModel;

            viewModel.FormsViewDidAppear(parameter);



            page.Appearing += async (object sender, EventArgs e) =>

            {

                await viewModel.FormsViewWillAppear(parameter);

            };



            page.Disappearing += async (object sender, EventArgs e) =>

            {

                await viewModel.FormsViewDisappearing();

            };

            return page;

        }



        protected Type GetPageTypeForViewModel(Type viewModelType)
        {

            if (!mappings.ContainsKey(viewModelType))

            {
                throw new KeyNotFoundException($"No map for ${viewModelType} was found on navigation mappings");
            }

            return mappings[viewModelType];

        }


        protected PopupPage CreateAndBindPopupPage(Type viewModelType, object parameter)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);

            if (pageType == null)
            {
                throw new Exception($"Mapping type for {viewModelType} is not a PopupPage");
            }

            PopupPage page = Activator.CreateInstance(pageType) as PopupPage;
            BaseViewModel viewModel = ViewModelLocator.Instance.Resolve(viewModelType) as BaseViewModel;

            viewModel.FormsViewDidLoadAsync(parameter);
            page.BindingContext = viewModel;
            viewModel.FormsViewWillAppear(parameter);

            page.Appearing += async (object sender, EventArgs e) =>
            {
                await viewModel.FormsViewDidAppear(parameter);
            };

            page.Disappearing += async (object sender, EventArgs e) =>
            {
                await viewModel.FormsViewDisappearing();
            };

            return page;
        }

        public Task<bool> RemovePreviousPage()
        {

            try
            {
                if (CurrentApplication.MainPage.Navigation.NavigationStack.Count > 2)
                    CurrentApplication.MainPage.Navigation.RemovePage(CurrentApplication.MainPage.Navigation.NavigationStack[CurrentApplication.MainPage.Navigation.NavigationStack.Count - 2]);
            }
            catch (Exception ex)
            {
                return Task.FromResult(false);
            }


            return Task.FromResult(true);
        }

        public Task<bool> RemoveToHomePage()
        {

            //Borra todas las páginas de la pila, desde la actual hasta llegar al principio (Generalmente el HomePage)

            try
            {
                for (int i = 0; i <= CurrentApplication.MainPage.Navigation.NavigationStack.Count - 2; i++)
                {
                    CurrentApplication.MainPage.Navigation.RemovePage(CurrentApplication.MainPage.Navigation.NavigationStack[CurrentApplication.MainPage.Navigation.NavigationStack.Count - 2]);
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }
    }
}
