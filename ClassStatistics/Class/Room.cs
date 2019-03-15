namespace Class
{
    public class Room
    {
        public string Building { get; private set; }
        public int Number { get; private set; }

        public Room(string building, int number)
        {
            this.Building = building;
            this.Number = number;
        }
    }
}