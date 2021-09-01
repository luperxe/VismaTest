using VismaTest.Languages;
using VismaTest.Services.HttpConnector;
using VismaTest.Services.Navigation;
using VismaTest.Services.WebServices.Interfaces;
using VismaTest.ViewModels.Base;
using VismaTest.ViewModels.GoogleAuth;
using VismaTest.ViewModels.Weather;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace VismaTest.ViewModels.Home
{
    public class HomeViewModel : BaseViewModel
    {
        #region commands

        protected Command loginCommand;

        public ICommand LoginCommand
        {
            get
            {
                return loginCommand = loginCommand ?? new Command(DoLoginHander);
            }
        }

        protected Command _navigateToWeatherCommand;
        public ICommand NavigateToWeatherCommand
        {
            get
            {
                return _navigateToWeatherCommand = _navigateToWeatherCommand ?? new Command(NavigateToWeather);
            }
        }

        protected Command _navigateToCurrentLocationWeatherCommand;
        public ICommand NavigateToCurrentLocationWeatherCommand
        {
            get
            {
                return _navigateToCurrentLocationWeatherCommand = _navigateToCurrentLocationWeatherCommand ?? new Command(NavigateToCurrentLocationWeather);
            }
        }

        #endregion

        public HomeViewModel(INavigationService navigationService, IHttpService webClient, IVTService vtService) : base(navigationService, webClient, vtService)
        {

        }

        public override Task FormsViewDidAppear(object parameters)
        {
            this.Title = "Home";
            return base.FormsViewDidAppear(parameters);
        }

        private void DoLoginHander(object obj)
        {
            this.navigationService.NavigateToAsync<GoogleAuthViewModel>();
        }

        private void NavigateToWeather(object obj)
        {
            this.navigationService.NavigateToAsync<WeatherViewModel>();
        }

        private void NavigateToCurrentLocationWeather(object obj)
        {
            this.navigationService.NavigateToAsync<CurrentLocationWeatherViewModel>();
            
        }
    }
}
