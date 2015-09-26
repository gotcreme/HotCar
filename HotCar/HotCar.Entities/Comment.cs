using System;

namespace HotCar.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public string Text { get; set; }
        public string SenderLogin { get; set; }
        public int TripId { get; set; }
    }
}
