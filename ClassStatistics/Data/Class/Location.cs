namespace Class
{
    public class Location
    {
        public int Capacity { get; private set; }
        public int RemainingSeats { get; private set; }
        public int Waitlist { get; private set; }
        public float Ratio { get => (Capacity - RemainingSeats) / (float)Capacity; }
        public Room Room { get; private set; }

        public Location(int capacity, int remainingSeats, int waitlist, Room room)
        {
            if (capacity < remainingSeats)
            {
                throw new System.ArgumentException($"There cannot be more remaining seats ({remainingSeats}) then the capacity ({capacity}) of the room!");
            }
            this.Capacity = capacity;
            this.RemainingSeats = remainingSeats;
            this.Room = room;
            this.Waitlist = waitlist;
        }
        public Location(int seats, int remainingSeats, int waitlist, string building, int number) : this(seats, remainingSeats, waitlist, new Room(building, number))
        {
        }
    }
}