
namespace Class
{
    public class Location
    {
        public int Capacity { get; private set; }
        public int RemainingSeats { get; private set; }
        public int Waitlist { get; private set; }
        public float Ratio { get => (Capacity - RemainingSeats) / (float)Capacity; }
        public Room Room { get; private set; }

        public Location(string ratio, int waitlist, Room room)
        {
            string[] value = ratio.Split("/");
            this.Capacity = System.Convert.ToInt32(value[1]);
            this.RemainingSeats = System.Convert.ToInt32(value[0]);
            this.Waitlist = waitlist;
            this.Room = room;
        }

        public Location(int capacity, int remainingSeats, int waitlist, Room room)
        {
            if (capacity < remainingSeats)
            {
                throw new System.ArgumentException($"There cannot be more remaining seats ({remainingSeats}) then the capacity ({capacity}) of the room!");
            }
            this.Capacity = capacity;
            this.RemainingSeats = remainingSeats;
            this.Waitlist = waitlist;
            this.Room = room;
        }
        
    }
}