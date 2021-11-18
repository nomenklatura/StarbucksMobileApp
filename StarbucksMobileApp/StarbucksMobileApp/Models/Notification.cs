using System.Collections.Generic;

namespace StarbucksMobileApp.Models
{
    public class Notification
    {
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool FooterShow { get; set; }
        public List<string> FooterButtonGroup { get; set; }

        public Notification()
        {
            FooterButtonGroup = new List<string>();
        }
    }
}
