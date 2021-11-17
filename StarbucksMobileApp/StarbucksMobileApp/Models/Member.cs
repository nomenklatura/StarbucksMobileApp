using System.Collections.Generic;

namespace StarbucksMobileApp.Models
{
    public class Member
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public float Balance { get; set; } = 0;
        public int Star { get; set; } = 0;
        public string Description { get; set; }
        public string Password { get; set; } = "123456";
        public bool IsPerson { get; set; } = false;

        public override string ToString()
        {
            return Name;
        }

    }
}
