namespace Class
{
    public class Room
    {
        public string Building { get; private set; }
        public int Number { get; private set; }
        public string Code { get => $"{Building}-{Number}"; }

        public Room(string building, int number)
        {
            this.Building = building;
            this.Number = number;
        }

        public Room(string code)
        {
            string[] values = code.Split('-');
            this.Building = values[0];
            this.Number = System.Convert.ToInt32(values[1]);
        }
    }
}