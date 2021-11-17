namespace StarbucksMobileApp.Helpers
{
    public interface ILocalize
    {
        void SetLocale();
        System.Globalization.CultureInfo GetCurrentCultureInfo();
    }
}
