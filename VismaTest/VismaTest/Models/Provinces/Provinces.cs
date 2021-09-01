using System;
using System.Collections.Generic;

namespace VismaTest.Models.Provinces
{
    public class Provinces
    {
        public List<Provincia> provincias { get; set; }
    }

    public class Provincia
    {
        public string CODPROV { get; set; }
        public string NOMBRE_PROVINCIA { get; set; }
        public string CODAUTON { get; set; }
        public string COMUNIDAD_CIUDAD_AUTONOMA { get; set; }
        public string CAPITAL_PROVINCIA { get; set; }
    }
}
