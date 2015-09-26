using System.Drawing;
using System.IO;

namespace HotCar.Entities
{
    public class Car
    {
        public int Id { get; set; }
        public byte[] Photo { get; set; }
        public string MakeCar { get; set; }
        public int OwnerId { get; set; }
    }
}
