using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_Lab1
{
    public class GameAccount
    {
        private string UserName;
        private int CurrentRating;
        public int GamesCount;
        private StringBuilder Stats;
        private Random rnd;
        public List<GameInfo> games;


        public GameAccount(string name, int rating)
        {
            UserName = name;
            CurrentRating = rating;
            GamesCount = 0;
           
            Stats = new StringBuilder("");
            rnd = new Random();
            games = new List<GameInfo>();
        }

        public  void WinGame(GameAccount opponentName, int rating) 
        {
            if (rating <= 0)
            {
                throw new IncorrectBetException("Incorrect bet. Bet should be more than 0");
            }

            if (opponentName.CurrentRating - rating < 0 || this.CurrentRating - rating < 0)
            {
                throw new IncorrectBetException("Bet is to big. One or both players don't have enough rating");
            }
            CurrentRating += rating;
            GamesCount++;
            
            
            int id = rnd.Next(1,100);
            games.Add(new GameInfo(GamesCount,id,rating,opponentName.UserName,GameResult.WON));
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
            CurrentRating -= rating;
            GamesCount++;
            
            int id = rnd.Next(1,100);
            games.Add(new GameInfo(GamesCount,id,rating,opponentName.UserName,GameResult.LOST));
        }

        public void GameStats()
        {
            Console.WriteLine();
            Console.WriteLine("                        Statistic for {0,6}",UserName);
            Console.WriteLine("|№ оf game | ID of game |         Opponents         |  Result | BET |");
            foreach (var info in games)
            {
                Console.WriteLine("|    {0,2}    |     {1,2}     |     {2,7} vs {3,7}    |  {4,4}   |  {5,2} |",info.GameNumber,info.IdGame,UserName,info.Opponent,info.ResultGame,info.Bet); 
            }
        }

        public void PlayerInfo()
        {
            int l = 0;
            int w = 0;
            foreach (var info in games)
            {
                if (info.ResultGame == GameResult.WON) w++;
                else l++;
            }
            Console.WriteLine();
            Console.WriteLine("| Name of Player |Amount of games | Amount of wins | Amount of loses |Rating |");
            Console.WriteLine("|     {0,6}     |       {1,2}       |        {2,2}      |        {3,2}       |  {4,3}  |",UserName,GamesCount,w,l,CurrentRating);
        }

        
    }
}