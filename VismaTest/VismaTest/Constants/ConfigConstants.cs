using System;
using System.Collections.Generic;
using System.Text;

namespace VismaTest.Constants
{
    public class ConfigConstants
    {

        #region Entorno

        private static VismaTest.Models.Enum.Environment currentEnvironment;
        public static string baseUrl;


        static ConfigConstants()
        {
            #if (DEBUG)
                   SetEnvironment(VismaTest.Models.Enum.Environment.Des);
            #else
                   SetEnvironment(EnvironmentEnum.Pro);
            #endif
        }

        private static void SetEnvironment(VismaTest.Models.Enum.Environment environment)
        {
            switch (environment)
            {
                case VismaTest.Models.Enum.Environment.Des:
                    baseUrl = "https://www.el-tiempo.net";
                    break;

                case VismaTest.Models.Enum.Environment.Pre:
                    baseUrl = "https://pre";
                    break;

                case VismaTest.Models.Enum.Environment.Pro:
                    baseUrl = "https://pro";
                    break;
            }
        }


        #endregion
    }
}
