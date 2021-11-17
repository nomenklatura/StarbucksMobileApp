namespace StarbucksMobileApp.Helpers
{
    public interface ICihazInfo
    {
        string GetSerialNumber();
        string GetOS();
        string GetLocalFilePath();
        string GetLocalFilePath(bool db);
        void SetStatusBarColor(string rgb);
    }
}
