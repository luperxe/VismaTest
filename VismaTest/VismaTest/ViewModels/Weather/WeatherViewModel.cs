using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using VismaTest.Models.Weather;
using VismaTest.Services.HttpConnector;
using VismaTest.Services.Navigation;
using VismaTest.Services.WebServices.Interfaces;
using VismaTest.ViewModels.Base;

namespace VismaTest.ViewModels.Weather
{
    public class WeatherViewModel : BaseViewModel
    {
        private ObservableCollection<VisualProvinceWeather> _listWeather;
        public ObservableCollection<VisualProvinceWeather> ListWeather
        {
            get
            {
                return this._listWeather;
            }

            set
            {
                this._listWeather = value;
                this.OnPropertyChanged();
            }
        }

        public WeatherViewModel(INavigationService navigationService, IHttpService webClient, IVTService vtService) : base(navigationService, webClient, vtService)
        {

        }

        public async override Task FormsViewDidAppear(object parameters)
        {
            this.IsBusy = true;
            await GetProvincesWithWeather();
            this.IsBusy = false;
        }

        private async Task GetProvincesWithWeather()
        {
            var list = new List<VisualProvinceWeather>();
            ListWeather = new ObservableCollection<VisualProvinceWeather>();

            var provinces =  await vtService.GetProvinces();

            if(provinces != null && provinces.Count > 0)
            {
                foreach(var province in provinces)
                {
                   var weather = await vtService.GetProvinceWeather(province.CODPROV);
                    if (weather?.ciudades != null)
                    {
                        var city = weather?.ciudades?.FirstOrDefault();
                        list.Add(new VisualProvinceWeather { Name = province?.NOMBRE_PROVINCIA, Min = string.Format("Min: {0} ºC", city?.temperatures?.min), Max = string.Format("Max: {0} ºC",city?.temperatures?.max) });
                    }
                }
            }

            ListWeather = new ObservableCollection<VisualProvinceWeather>(list);
        }
    }
}
