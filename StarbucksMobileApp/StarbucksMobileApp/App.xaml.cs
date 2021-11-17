using Microsoft.AppCenter.Crashes;
using StarbucksMobileApp.Api.ClientManagers;
using StarbucksMobileApp.Api.DataStorage;
using StarbucksMobileApp.Helpers;
using StarbucksMobileApp.Resources.Images;
using StarbucksMobileApp.Resources.Languages;
using System;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;

namespace StarbucksMobileApp
{
    public partial class App : Application
    {
        public static ImageSource noImage { get; } = ImageSource.FromResource("MyaKartSerez.Resources.Images.profile_images.png", typeof(ImageResourceExtension).GetTypeInfo().Assembly);

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();

            DependencyService.Get<ILocalize>().SetLocale();
            var lang = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            if (lang.Name.Contains("tr"))
            {
                LanguageResource.Culture = new System.Globalization.CultureInfo("tr-TR");
            }
            else
            {
                LanguageResource.Culture = new System.Globalization.CultureInfo("en-US");
            }

            try
            {
                if (!Application.Current.Properties.ContainsKey(AppEnums.COOKIE_MEMBER_ISLOGIN))
                {

                    if (Application.Current.Properties.ContainsKey(AppEnums.COOKIE_MEMBER_EMAIL))
                    {
                        var musteriEmail = Convert.ToString(Application.Current.Properties[AppEnums.COOKIE_MEMBER_EMAIL]);
                        var musteriPhone = Convert.ToString(Application.Current.Properties[AppEnums.COOKIE_MEMBER_PHONE]);
                        var musteriPassword = Convert.ToString(Application.Current.Properties[AppEnums.COOKIE_MEMBER_PASSWORD]);

                        if ((String.IsNullOrEmpty(musteriEmail) || String.IsNullOrEmpty(musteriPhone)) & String.IsNullOrEmpty(musteriPassword))
                        {

                        }
                        else
                        {
                            var email = String.IsNullOrEmpty(musteriEmail) ? "" : musteriEmail.Trim();
                            var phone = String.IsNullOrEmpty(musteriPhone) ? "" : musteriPhone.Trim().Replace(" ", "");
                            var psw = String.IsNullOrEmpty(musteriPassword) ? "" : musteriPassword.Trim().Replace(" ", "");

                            LoginClientManager loginClient = new LoginClientManager(DataContext.ApiUrl);
                            var _member = loginClient.Login(psw, email, phone);
                            if (_member != null)
                            {
                                if (!_member.Errors.Any())
                                {
                                    //AppHelper.member = _mapper.Map<Member>(_member);
                                    AppHelper.member = new Models.Member
                                    {
                                        Balance = _member.Balance,
                                        Description = _member.Description,
                                        Email = _member.Email,
                                        IsPerson = _member.IsPerson,
                                        Name = _member.Name,
                                        Phone = _member.Phone,
                                        Star = _member.Star
                                    };
                                    AppHelper.SetCookie(AppEnums.COOKIE_MEMBER_EMAIL, AppHelper.member.Email.ToString());
                                    AppHelper.SetCookie(AppEnums.COOKIE_MEMBER_PHONE, AppHelper.member.Phone.ToString());
                                    AppHelper.SetCookie(AppEnums.COOKIE_MEMBER_PASSWORD, AppHelper.member.Password.ToString());   
                                }
                            }                            
                        }
                    }
                }
            }
            catch (Exception err)
            {
                Crashes.TrackError(err);
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
