using System;
using System.Text;

namespace OOP_Lab1
{
    public class GameAccount
    {
        private string UserName;
        private int CurrentRating;
        public int GamesCount;
        private int WinsCount;
        private int LoseCount;
        private StringBuilder Stats;
        private Random rnd;
        public GameList<String> games;
        
       
        public GameAccount(string name, int rating)
        {
            UserName = name;
            CurrentRating = rating;
            GamesCount = 0;
            WinsCount = 0;
            LoseCount = 0;
            Stats = new StringBuilder("");
            rnd = new Random();
            games = new GameList<String>();
        }

        public  void WinGame(GameAccount opponentName, int rating) 
        {
            if (rating <= 0)
            {
                throw new IncorrectBetException("Incorrect bet. Bet should be more than 0");
            }
            if (opponentName.CurrentRating-rating < 0 || this.CurrentRating-rating < 0)
            {
                throw new IncorrectBetException("Bet is to big. One or both players don't have enough rating");
            }
            var stats = new StringBuilder("");
            this.CurrentRating += rating;
            this.GamesCount++;
            this.WinsCount++;
            opponentName.CurrentRating -= rating;
            opponentName.GamesCount++;
            opponentName.LoseCount++;
            int id = rnd.Next(1,100);
            Stats.Append($"|   {this.GamesCount,2}     |     {id,2}     | {this.UserName,6} vs {opponentName.UserName,6} | {this.UserName,6} has {GameResults.WON,4} | {rating,3} |");
            games.AddGame(id,Stats.ToString());
            Stats.Remove(0, Stats.Length);
            Stats.Append($"|   {opponentName.GamesCount,2}     |     {id,2}     | {opponentName.UserName,6} vs {this.UserName,6} | {opponentName.UserName,6} has {GameResults.LOST,4} | {rating,3} |");
            opponentName.games.AddGame(id,Stats.ToString());
            Stats.Remove(0, Stats.Length);
        }
        public  void LoseGame(GameAccount opponentName, int rating)
        {
            if (rating <= 0)
            {
                throw new IncorrectBetException("Incorrect bet. Bet should be more than 0");
            }
            if (opponentName.CurrentRating-rating < 0 || this.CurrentRating-rating < 0)
            {
                throw new IncorrectBetException("Bet is to big. One or both players don't have enough rating");
            }
            this.CurrentRating -= rating;
            this.GamesCount++;
            this.LoseCount++;
            opponentName.CurrentRating += rating;
            opponentName.GamesCount++;
            opponentName.WinsCount++;
            int id = rnd.Next(1,100);
            Stats.Append($"|   {this.GamesCount,2}     |     {id,2}     | {this.UserName,6} vs {opponentName.UserName,6} | {this.UserName,6} has {GameResults.LOST,4} | {rating,3} |");
            games.AddGame(id, Stats.ToString());
            Stats.Remove(0, Stats.Length);
            Stats.Append($"|   {opponentName.GamesCount,2}     |     {id,2}     | {opponentName.UserName,6} vs {this.UserName,6} | {opponentName.UserName,6} has {GameResults.WON,4} | {rating,3} |");
            opponentName.games.AddGame(id,Stats.ToString());
            Stats.Remove(0, Stats.Length);
        }

        public void GameStats()
        {
            Console.WriteLine();
            Console.WriteLine("                        Statistic for {0,6}",this.UserName);
            Console.WriteLine("|№ оf game | ID of game |     Opponents    |      Result     | BET |");
            games.AllStatistic();
        }

        public void PlayerInfo()
        {
            Console.WriteLine();
            Console.WriteLine("| Name of Player |Amount of games | Amount of wins | Amount of loses |Rating |");
            Console.WriteLine("|     {0,6}     |       {1,2}       |        {2,2}      |        {3,2}       |  {4,3}  |",this.UserName,this.GamesCount,this.WinsCount,this.LoseCount,this.CurrentRating);
        }

        public void OverAllInfo()
        {
            this.GameStats();
            this.PlayerInfo();
        }
    }
}