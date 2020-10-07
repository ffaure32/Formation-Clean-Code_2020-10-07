namespace Trivia
{
    public class Player
    {
        public bool InPenaltyBox { get; set; } = false;
        public string Name { get; private set; }

        public int Place { get; set; } = 0;

        public int Purse { get; set; } = 0;

        public Player(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
