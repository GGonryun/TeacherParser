namespace Class
{
    public class Location
    {
        public int Capacity { get; private set; }
        public int RemainingSeats { get; private set; }
        public float Ratio { get => (Capacity - RemainingSeats) / (float)Capacity; }
        public Room Room { get; private set; }

        public Location(int seats, int remainingSeats, Room room)
        {
            this.Capacity = seats;
            this.RemainingSeats = remainingSeats;
            this.Room = room;
        }
        public Location(int seats, int remainingSeats, string building, int number) : this(seats, remainingSeats, new Room(building, number))
        {
        }
    }
}