using System;
using System.Collections.Generic;
using VismaTest.Models.Provinces;

namespace VismaTest.Models.Weather
{
    public class ProvinceWeather
    {
        public List<Ciudades> ciudades { get; set; }
        public Provincia provincia { get; set; }

        public class Temperatures
        {
            public string max { get; set; }
            public string min { get; set; }
        }

        public class Ciudades
        {
            public string id { get; set; }
            public string idProvince { get; set; }
            public string name { get; set; }
            public string nameProvince { get; set; }
            public Temperatures temperatures { get; set; }
        }              
    }
}
