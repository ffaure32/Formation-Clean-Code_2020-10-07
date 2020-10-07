namespace Trivia
{
    public class Player
    {
        public bool InPenaltyBox { get; set; } = false;
        public string Name { get; }

        public int Place { get; set; } = 0;

        public int Purse { get; private set; } = 0;

        public Player(string name)
        {
            Name = name;
        }

        public void AddPoint()
        {
            Purse++;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
