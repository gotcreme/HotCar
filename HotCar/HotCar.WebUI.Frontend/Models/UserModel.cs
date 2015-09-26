using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using HotCar.Entities;

namespace HotCar.WebUI.Frontend.Models
{
    public class UserModel
    {
        public User UserInfo { get; set; }
        public Trip[] ActualTrips { get; set; }
        public Trip[] OutDatedTrips { get; set; }
        public Comment[] CommentsAboutYou { get; set; }
        public Comment[] YourComments { get; set; }

        //Application[] Applications { get; set;}
    }
}