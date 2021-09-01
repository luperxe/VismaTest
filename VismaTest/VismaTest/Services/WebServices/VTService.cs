using VismaTest.Constants;
using VismaTest.Models.Enum;
using VismaTest.Models.Generic;
using VismaTest.Models.Provinces;
using VismaTest.Models.Weather;
using VismaTest.Services.HttpConnector;
using VismaTest.Services.WebServices.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Auth;
using VismaTest.Models.GoogleAuth;

namespace VismaTest.Services.WebServices
{
    public class VTService : BaseWebService, IVTService
    {
        public VTService(IHttpService conection) : base(conection)
        {
        }

        public async Task<List<Provincia>>GetProvinces()
        {
            var provincesList = new List<Provincia>();

            string url = BaseUrlServices(EndPointConstants.Provinces);           

            var content = await this.httpService.SendAsync<ErrorResponse>(method: CustomHttpMethod.Get, url: url);

            if (!string.IsNullOrEmpty(content))
            {
                var response = JsonConvert.DeserializeObject<Provinces>(content);
                provincesList = response.provincias;
            }

            return provincesList;
        }

        public async Task<ProvinceWeather> GetProvinceWeather(string codProv)
        {
            var weather = new ProvinceWeather();

            string url = string.Concat(BaseUrlServices(EndPointConstants.ProvinceWeather), codProv);

            try
            {
                var content = await this.httpService.SendAsync<ErrorResponse>(method: CustomHttpMethod.Get, url: url);

                if (!string.IsNullOrEmpty(content))
                {
                    var response = JsonConvert.DeserializeObject<ProvinceWeather>(content);
                    weather = response;
                }
            }catch(Exception e)
            {

            }

            return weather;
        }

        public async Task<CurrentLocationWeather> GetCurrentLocationWeather(Location location)
        {
            var weather = new CurrentLocationWeather();

            string url = string.Format(EndPointConstants.CurrentLocationWeather, location.Latitude, location.Longitude);

            try
            {
                var content = await this.httpService.SendAsync<ErrorResponse>(method: CustomHttpMethod.Get, url: url);

                if (!string.IsNullOrEmpty(content))
                {
                   var response = JsonConvert.DeserializeObject<CurrentLocationWeather>(content);
                    weather = response;
                }
            }
            catch (Exception e)
            {

            }

            return weather;
        }

        public async Task<User> GetGoogleAuthUser(AuthenticatorCompletedEventArgs e)
        {
            var user = new User();

            var request = new OAuth2Request("GET", new Uri(GoogleAuthConstants.UserInfoUrl), null, e.Account);
            var response = await request.GetResponseAsync();
            if (response != null)
            {
                string userJson = await response.GetResponseTextAsync();
                user = JsonConvert.DeserializeObject<User>(userJson);
            }

            return user;
        }
    }
}
