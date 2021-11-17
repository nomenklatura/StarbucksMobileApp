using Microsoft.AppCenter.Crashes;
using StarbucksMobileApp.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace StarbucksMobileApp.Helpers
{
    public static class AppHelper
    {
        public static Member member = null;

        public static string GetCookie(string key)
        {
            string r = "";

            if (Application.Current.Properties.ContainsKey(key))
            {
                r = Application.Current.Properties[key].ToString();
            }

            return r;
        }
        public static void SetCookie(string key, string val)
        {
            Application.Current.Properties[key] = "" + val;
            Application.Current.SavePropertiesAsync();
        }
        public static void SetCookieClear()
        {
            Application.Current.Properties.Clear();
            Application.Current.SavePropertiesAsync();
        }

        public static async void Animate(View view)
        {
            await view.TranslateTo(10, 0, 100, Easing.SinIn);
            await view.TranslateTo(-10, 0, 100, Easing.SinIn);
            await view.TranslateTo(10, 0, 100, Easing.SinIn);
            await view.TranslateTo(-10, 0, 100, Easing.SinIn);
            await view.TranslateTo(0, 0, 100, Easing.SinIn);
        }

        public static string GetErrorMessage(Exception err, bool console = true, bool appCenter = true, Dictionary<string, string> appCenterProperties = null)
        {
            string message = err.Message;
            if (err.InnerException != null)
            {
                message += "\n\t->" + err.InnerException.Message;
            }

            if (console)
            {
                Console.WriteLine(message);
            }

            if (appCenter)
            {
                Crashes.TrackError(err, appCenterProperties);
            }

            return message;
        }
    }
}
