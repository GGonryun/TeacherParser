namespace Class
{
    public class Room
    {
        public string Building { get; private set; }
        public string Number { get; private set; }
        public string Code { get => $"{Building}-{Number}"; }

        public Room(string building, string number)
        {
            this.Building = building;
            this.Number = number;
        }

        public Room(string code)
        {
            if(code.Length < 1)
            {
                this.Building = "";
                this.Number = "";
            }
            else
            {
                string[] values = code.Split('-');
                this.Building = values[0];
                this.Number = values[1];
            }
            
        }
    }
}