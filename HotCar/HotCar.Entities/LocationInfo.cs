using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotCar.Entities
{
    public class LocationInfo
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public LocationInfo(double lat, double longt)
        {
            Longitude = longt;
            Latitude = lat;
        }
    }
}
