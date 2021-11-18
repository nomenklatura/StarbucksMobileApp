using StarbucksMobileApp.Models;
using System.Collections.Generic;

namespace StarbucksMobileApp.Api.DataStorage
{
    public static class DataContext
    {
        public static string ApiUrl { get; private set; } = "https://192.168.1.250:1234/api/";

        public static List<Member> Members { get; set; } = new List<Member>
        {            
            new Member { Name = "Misafir 1", Email="misafir1@mail.com",Phone="5001112201", Password = "123456", Balance=120, Star=10, Description = "", IsPerson = false},
            new Member { Name = "Misafir 2", Email="misafir2@mail.com",Phone="5001112202", Password = "123456", Balance=66, Star=8, Description = "IT", IsPerson = true},
            new Member { Name = "Misafir 3", Email="misafir3@mail.com",Phone="5001112203", Password = "123456", Balance=75, Star=140, Description = "", IsPerson = false},
        };

        public static List<Notification> Notifications { get; set; } = new List<Notification>
        {
            new Notification 
            { 
                ImageUrl = "https://www.starbucks.com.tr/media/yeni-yil-heyecani-starbucks-bardaginda-web_tcm95-75085.png",
                Title="Yeni Yıl Heyecanı",
                Description="Yeni Yıl Heyecanı Starbuck bardağında.",
                FooterShow=false
            },

        };
    }
}
