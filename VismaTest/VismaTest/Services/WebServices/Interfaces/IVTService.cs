using VismaTest.Models.Provinces;
using VismaTest.Models.Weather;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using VismaTest.Models.GoogleAuth;
using Xamarin.Auth;

namespace VismaTest.Services.WebServices.Interfaces
{
    public interface IVTService
    {
        Task<List<Provincia>> GetProvinces();

        Task<ProvinceWeather> GetProvinceWeather(string codProv);

        Task<CurrentLocationWeather> GetCurrentLocationWeather(Location location);

        Task<User> GetGoogleAuthUser(AuthenticatorCompletedEventArgs e);
    }
}
