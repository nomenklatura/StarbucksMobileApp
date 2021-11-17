using StarbucksMobileApp.Resources.Images;
using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using Xamarin.Forms;

namespace StarbucksMobileApp.Helpers
{
    public class ConverterModel : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter.ToString() == "BYTE_TO_IMAGE")
            {
                if (value == null)
                    return App.noImage;

                return ImageSource.FromStream(() => new MemoryStream((byte[])value));
            }
            else if (parameter.ToString() == "EMBEDDED_TO_IMAGE")
            {
                if (value == null)
                    return App.noImage;

                return ImageSource.FromResource(value.ToString(), typeof(ImageResourceExtension).GetTypeInfo().Assembly);
            }
            else if ((string)parameter == "FIRSTNAME")
            {
                string[] str = value.ToString().Split(' ');
                if (str.Length > 1)
                {
                    string name = value.ToString().Replace(" " + str[str.Length - 1], "");
                    return name;
                }

                return value;
            }
            else if ((string)parameter == "LASTNAME")
            {
                string[] str = value.ToString().Split(' ');
                if (str.Length > 1)
                {
                    string name = str[str.Length - 1];
                    return name;
                }

                return value;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
