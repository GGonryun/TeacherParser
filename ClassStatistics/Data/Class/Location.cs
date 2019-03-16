namespace Class
{
    public class Location
    {
        public int Capacity { get; private set; }
        public int RemainingSeats { get; private set; }
        public int Waitlist { get; private set; }
        public float Ratio { get => (Capacity - RemainingSeats) / (float)Capacity; }
        public Room Room { get; private set; }

        public Location(int seats, int remainingSeats, int waitlist, Room room)
        {
            this.Capacity = seats;
            this.RemainingSeats = remainingSeats;
            this.Room = room;
            this.Waitlist = waitlist;
        }
        public Location(int seats, int remainingSeats, int waitlist, string building, int number) : this(seats, remainingSeats, waitlist, new Room(building, number))
        {
        }
    }
}