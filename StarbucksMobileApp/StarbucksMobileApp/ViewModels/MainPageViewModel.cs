using Microsoft.AppCenter.Crashes;
using StarbucksMobileApp.Api.ClientManagers;
using StarbucksMobileApp.Api.DataStorage;
using StarbucksMobileApp.Helpers;
using StarbucksMobileApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace StarbucksMobileApp.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private NotificationClientManager notificationClientManager;

        #region Notifications
        private ObservableCollection<Notification> notifications;
        public ObservableCollection<Notification> Notifications 
        {
            get => notifications;
            set
            {
                if (notifications!=value)
                {
                    notifications = value;
                    OnPropertyChanged(nameof(Notifications));
                }
            }
        }
        void GetNotifications()
        {
            try
            {
                notifications.Clear();
                var list = notificationClientManager.GetList();
                foreach (var item in list)
                {
                    notifications.Add(item);
                }

                OnPropertyChanged(nameof(Notifications));
            }
            catch (Exception err)
            {
                Crashes.TrackError(err);
            }
        }
        #endregion

        #region Welcome
        private string welcome;
        public string Welcome
        {
            get => welcome;
            set
            {
                if (welcome != value)
                {
                    welcome = value;
                    OnPropertyChanged(nameof(Welcome));
                }
            }
        }
        private void SetWelcome()
        {
            int hour = DateTime.Now.Hour;
            string memberFirstName = "";
            string w = "";
            string icon = "";

            if (hour>=0 & hour < 12)
            {
                w = "Günaydın.";
                icon = "🌞";
            }
            else if (hour >= 11 & hour < 18)
            {
                w = "İyi günler.";
                icon = "😎";
            }
            else
            {
                w = "İyi akşamlar.";
                icon = "🌙";
            }

            isLogin = false;
            if (AppHelper.member!=null)
            {
                string[] str = AppHelper.member.Name.Split(' ');
                if (str.Length > 1)
                    memberFirstName = AppHelper.member.Name.Replace(" " + str[str.Length - 1], "");
                else
                    memberFirstName = AppHelper.member.Name;

                isLogin = true;
            }

            welcome = $"{memberFirstName} {w} {icon}";

            OnPropertyChanged(nameof(Welcome));
            OnPropertyChanged(nameof(IsLogin));
        }
        #endregion

        #region IsLogin
        private bool isLogin;
        public bool IsLogin
        {
            get => isLogin;
            set
            {
                if (isLogin != value)
                {
                    isLogin = value;
                    OnPropertyChanged(nameof(IsLogin));
                }
            }
        }
        #endregion

        public MainPageViewModel()
        {
            notificationClientManager = new NotificationClientManager(DataContext.ApiUrl);
            notifications = new ObservableCollection<Notification>();
            GetNotifications();

            SetWelcome();
        }
    }
}
