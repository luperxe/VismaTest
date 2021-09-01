using System;
using System.Collections.Generic;
using System.Text;

namespace VismaTest.Constants
{
    public class EndPointConstants
    {
        public const string Provinces = "/api/json/v2/provincias";

        public const string ProvinceWeather = "/api/json/v2/provincias/";

        public const string CurrentLocationWeather = "https://api.openweathermap.org/data/2.5/weather?lat={0}&lon={1}&appid=20c242b6a6f46cd7216a0553c7d3cf46&units=metric";
        public const string CurrentLocationWeatherIcon = "http://openweathermap.org/img/wn/{0}@2x.png";                
    }
}
