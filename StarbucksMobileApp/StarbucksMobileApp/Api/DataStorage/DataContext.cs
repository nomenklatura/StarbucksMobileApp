using StarbucksMobileApp.Models;
using System.Collections.Generic;

namespace StarbucksMobileApp.Api.DataStorage
{
    public static class DataContext
    {
        public static string ApiUrl { get; private set; } = "127.0.0.1:1234";

        public static List<Member> Members { get; set; } = new List<Member>
        {
            new Member { Name = "Ali Haydar Durmuş", Email="ali@mail.com",Phone="5326451865", Balance=1500, Star=325, Description = "", IsPerson = false},
            new Member { Name = "Fırat Kuran", Email="firat@mail.com",Phone="5354794749", Balance=857, Star=150, Description = "", IsPerson = false},
            new Member { Name = "Ayşe Özgür", Email="ayse@mail.com",Phone="5526710408", Balance=333, Star=69, Description = "", IsPerson = true},
            new Member { Name = "Halit Özdemir", Email="halit@mail.com",Phone="5323036862", Balance=147, Star=15, Description = "", IsPerson = true},
            new Member { Name = "Erdem Aydın", Email="erdem@mail.com",Phone="5322941102", Balance=652, Star=998, Description = "", IsPerson = true},
            new Member { Name = "Misafir 1", Email="misafir1@mail.com",Phone="5001112233", Balance=0, Star=10, Description = "", IsPerson = false},
            new Member { Name = "Misafir 2", Email="misafir2@mail.com",Phone="5001112234", Balance=0, Star=10, Description = "", IsPerson = false},
            new Member { Name = "Misafir 3", Email="misafir2@mail.com",Phone="5001112235", Balance=0, Star=10, Description = "", IsPerson = false},
        };
    }
}
