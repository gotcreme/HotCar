namespace HotCar.Entities
{
    public class Passenger
    {
        public int Id { get; set; }
        public string PassengerLogin { get; set; }
        public int CountReservedSeats { get; set; }
        public int TripId { get; set; }
        public int Cost { get; set; }
        public string PassengerRoute { get; set; }
        public int CommentId { get; set; }
    }
}
