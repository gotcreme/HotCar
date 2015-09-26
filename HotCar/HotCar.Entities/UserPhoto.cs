using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotCar.Entities
{
    public class UserPhoto
    {
        public int Id { get; set; }
        public byte[] Photo { get; set; }
        public string FileExtension { get; set; }
    }
}
