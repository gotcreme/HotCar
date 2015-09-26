using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotCar.WebUI.Frontend.Models
{
    public class FiltersModel
    {
        public bool HideWithNoSeats { get; set; }

        public bool HideWithNoPhoto { get; set; }

        public string Date { get; set; }

        public int FromHour { get; set; }

        public int ToHour { get; set; }
    }
}