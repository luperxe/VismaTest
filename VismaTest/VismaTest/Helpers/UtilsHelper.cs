using VismaTest.Constants;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VismaTest.Helpers
{
    public class UtilsHelper
    {
      
        public static bool SetAppLanguage(string persistedLang)
        {

            Preferences.Set(PreferenceConstants.LANGUAGE_APP, persistedLang);
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo(persistedLang);
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo(persistedLang);

            return true;

        }


        /// <summary>
        /// Establece la cultura/idioma al arrancar la aplicación. 
        /// </summary>
        public static void SetInitialAppLanguage()
        {

            string persistedLang = Preferences.Get(PreferenceConstants.LANGUAGE_APP, string.Empty);
            if (string.IsNullOrEmpty(persistedLang))
            {
                persistedLang = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
                Preferences.Set(PreferenceConstants.LANGUAGE_APP, persistedLang);
            }

            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo(persistedLang);
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo(persistedLang);

        }

        public static void ShowMessage(string message)
        {
            Application.Current.MainPage.DisplayAlert("Alert", message, "OK");
        }
    }
}
