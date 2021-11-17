using Foundation;
using StarbucksMobileApp.Helpers;
using StarbucksMobileApp.iOS.Helpers.DependencyServices;
using System;
using System.Globalization;
using System.Threading;
using Xamarin.Forms;

[assembly: Dependency(typeof(Localize))]
namespace StarbucksMobileApp.iOS.Helpers.DependencyServices
{
    public class Localize : ILocalize
    {
        public CultureInfo GetCurrentCultureInfo()
        {
            var netLanguage = "en";
            var prefLang = "en";

            if (NSLocale.PreferredLanguages.Length > 0)
            {
                var pref = NSLocale.PreferredLanguages[0];
                prefLang = pref.Substring(0, 2);
                netLanguage = pref.Replace("_", "-");
            }

            CultureInfo ci = null;
            try
            {
                ci = new CultureInfo(netLanguage);
            }
            catch (Exception)
            {
                ci = new CultureInfo(prefLang);
            }

            return ci;
        }

        public void SetLocale()
        {
            var iosLocaleAuto = NSLocale.AutoUpdatingCurrentLocale.LocaleIdentifier;
            var netLocale = iosLocaleAuto.Replace("_", "-");
            CultureInfo ci;
            try
            {
                ci = new CultureInfo(netLocale);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ci = null;
            }

            if (ci != null)
            {
                Thread.CurrentThread.CurrentCulture = ci;
                Thread.CurrentThread.CurrentUICulture = ci;
            }
        }
    }
}