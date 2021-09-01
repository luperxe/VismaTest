using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VismaTest.Helpers;
using VismaTest.Models.Weather;
using VismaTest.Services.HttpConnector;
using VismaTest.Services.Navigation;
using VismaTest.Services.WebServices.Interfaces;
using VismaTest.ViewModels.Base;
using Xamarin.Essentials;

namespace VismaTest.ViewModels.Weather
{
    public class CurrentLocationWeatherViewModel : BaseViewModel
    {
        #region properties
        private string _name;
        public string Name
        {
            get
            {
                return this._name;
            }

            set
            {
                this._name = value;
                this.OnPropertyChanged();
            }
        }

        private string _currentTemp;
        public string CurrentTemp
        {
            get
            {
                return this._currentTemp;
            }

            set
            {
                this._currentTemp = value;
                this.OnPropertyChanged();
            }
        }

        private string _min;
        public string Min
        {
            get
            {
                return this._min;
            }

            set
            {
                this._min = value;
                this.OnPropertyChanged();
            }
        }

        private string _max;
        public string Max
        {
            get
            {
                return this._max;
            }

            set
            {
                this._max = value;
                this.OnPropertyChanged();
            }
        }

        private string _imageUri;
        public string ImageUri
        {
            get
            {
                return this._imageUri;
            }

            set
            {
                this._imageUri = value;
                this.OnPropertyChanged();
            }
        }

        private string _weatherDescription;
        public string WeatherDescription
        {
            get
            {
                return this._weatherDescription;
            }

            set
            {
                this._weatherDescription = value;
                this.OnPropertyChanged();
            }
        }



        #endregion

        public CurrentLocationWeatherViewModel(INavigationService navigationService, IHttpService webClient, IVTService vtService) : base(navigationService, webClient, vtService)
        {

        }

        public async override Task FormsViewDidAppear(object parameters)
        {
           var info = await GetCurrentLocation();

            var icon = info?.weather?.FirstOrDefault()?.icon;
            var urlIcon = "http://openweathermap.org/img/wn/{0}@2x.png";

            Name = info?.name;
            CurrentTemp = string.Format("{0:0.#} ºC", info?.main?.temp);
            Min = string.Format("{0:0.#} ºC", info?.main?.temp_min);
            Max = string.Format("{0:0.#} ºC", info?.main?.temp_max);
            WeatherDescription = info?.weather?.FirstOrDefault()?.description;
            ImageUri = icon != null ? string.Format(urlIcon, icon) : string.Empty;

        }

        private async Task<CurrentLocationWeather> GetCurrentLocation()
        {
            var info = new CurrentLocationWeather();

            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                var cts = new CancellationTokenSource();
                var location = await Geolocation.GetLocationAsync(request, cts.Token);

                if (location != null)
                {
                   info = await vtService.GetCurrentLocationWeather(location);
                }
                else
                {
                    UtilsHelper.ShowMessage("There was an error getting your location. Please, try again later");
                }
            }
            catch (FeatureNotSupportedException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (FeatureNotEnabledException ex)
                {
                Console.WriteLine(ex.Message);
            }
            catch (PermissionException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return info;
        }
    }
}
