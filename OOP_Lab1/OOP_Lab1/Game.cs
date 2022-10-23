namespace OOP_Lab1
{
    public class Game
    {
        
        public void Play(GameAccount player, GameAccount opponent, int rating,int key)
        {
            switch(key)
            {
                case 1 :
                    player.WinGame(opponent,rating);
                    opponent.LoseGame(player,rating);
                    break;
                case 2 : 
                    player.LoseGame(opponent,rating);
                    opponent.WinGame(player,rating);
                    break;
            }
        }
    }
}