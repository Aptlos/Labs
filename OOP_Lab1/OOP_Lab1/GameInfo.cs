namespace OOP_Lab1
{
    public class GameInfo
    {
        public int GameNumber { get; }
        public int IdGame { get; }
        public string Opponent { get; }
        public GameResult ResultGame { get; }
        public int Bet { get; }
        
        public GameInfo(int count, int id, int rating, string opponent, GameResult result)
        {
            GameNumber = count;
            IdGame = id;
            Bet = rating;
            Opponent = opponent;
            ResultGame = result;
        }
    }
}