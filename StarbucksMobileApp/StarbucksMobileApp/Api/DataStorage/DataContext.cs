using StarbucksMobileApp.Models;
using System.Collections.Generic;

namespace StarbucksMobileApp.Api.DataStorage
{
    public static class DataContext
    {
        public static string ApiUrl { get; private set; } = "127.0.0.1:1234";

        public static List<Member> Members { get; set; } = new List<Member>
        {            
            new Member { Name = "Misafir 1", Email="misafir1@mail.com",Phone="5001112201", Balance=120, Star=10, Description = "", IsPerson = false},
            new Member { Name = "Misafir 2", Email="misafir2@mail.com",Phone="5001112202", Balance=66, Star=8, Description = "IT", IsPerson = true},
            new Member { Name = "Misafir 3", Email="misafir3@mail.com",Phone="5001112203", Balance=75, Star=140, Description = "", IsPerson = false},
        };
    }
}
